using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer;

/// <summary>
/// 徽章（Medal）装备：佩戴 / 替换 / 卸下 + 按账号持久化。
///
/// 为什么要有这一层
/// ----------------
/// 「有徽章列表」和「能佩戴徽章」是两条完全不同的流程：
///   GetMedalDataReq 449 → GetMedalDataRsp 450   收藏 → 勋章，返回拥有列表
///   MedalOpReq      451 → MedalOpRsp      452   佩戴 / 替换 / 卸下
/// 服务端原来只有 449 的 handler，451 一条 `not handled`；
/// 而 GetMainDataRsp.on_medal_id（tag 24）又是写死 0。
/// 于是：装备请求没人应答；即使临时回一个成功包，刷新或重登后主数据把它顶回 0，
/// 界面立刻变回「未佩戴」。
///
/// 所以修复必须三件事一起做，缺一不可：
///   1. 收下 451 并处理 wear / off
///   2. 把每个账号的选择落库（独立的 MedalEquipment 表，不塞进 User 缓存）
///   3. GetMainDataRsp.on_medal_id 改成读这里，且 450 的列表与这里口径一致
///
/// 限时勋章策略（本地服自定，不是官方规则）
/// ----------------------------------------
/// MedalData.json 130 枚里 25 枚是 timeType=2。客户端会把「end_time 已过」的勋章画成灰色，
/// 所以不能一律回 0（那会被当成永久，也还行）也不能回真实历史时间（全过期）。
/// 这里在账号**首次取数**时生成一个「now + 999 天」的截止时间并落库，
/// 之后读列表、切徽章、重启服务端都不再续期 —— 同一个账号的截止时间是稳定的。
/// </summary>
public static class MedalService
{
	/// <summary>
	/// MedalEquipment 表的一行：一个账号一条。
	///
	/// 为什么独立建表而不是给 UserScheme 加字段：
	/// 旧 User 对象在会话里被缓存/反序列化，加字段容易在某个写入路径上被旧值覆盖；
	/// 独立表 + 唯一索引能保证「谁的就是谁的」，而且读的时候不依赖会话状态。
	/// </summary>
	public class MedalEquipmentScheme : IRow
	{
		public long Id { get; set; }

		public long CreatedAt { get; set; }

		/// <summary>账号 uid，唯一索引。</summary>
		public uint OwnerUid { get; set; }

		/// <summary>当前佩戴的勋章 id；0 = 未佩戴。</summary>
		public uint OnMedalId { get; set; }

		/// <summary>限时勋章共用的截止时间（Unix 秒）；首次取数时生成，之后不再变。</summary>
		public long LimitedEndTime { get; set; }

		public void Save()
		{
			collection.ReplaceOne(this);
		}
	}

	/// <summary>限时勋章发放天数（本地服策略）。</summary>
	public const int LimitedDays = 999;

	/// <summary>写入串行化：同一进程内并发改同一账号时不会互相覆盖。</summary>
	private static readonly object _gate = new();

	public static readonly SqlTable<MedalEquipmentScheme> collection =
		Global.Table<MedalEquipmentScheme>("MedalEquipment",
			("owner_uid", "INTEGER", (MedalEquipmentScheme x) => x.OwnerUid, true));

	private static long Now => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

	/// <summary>
	/// 取（或补）某账号的装备记录。首次访问时顺手生成限时截止时间。
	/// </summary>
	public static MedalEquipmentScheme FromUid(uint uid)
	{
		MedalEquipmentScheme exist = collection.All().FirstOrDefault(x => x.OwnerUid == uid);
		if (exist != null)
		{
			// 老行（比如手工插过）没截止时间就补一个，避免限时勋章全被判过期
			if (exist.LimitedEndTime <= 0)
			{
				exist.LimitedEndTime = Now + LimitedDays * 86400L;
				exist.Save();
			}
			return exist;
		}

		MedalEquipmentScheme created = new MedalEquipmentScheme
		{
			OwnerUid = uid,
			OnMedalId = 0u,
			LimitedEndTime = Now + LimitedDays * 86400L
		};
		collection.InsertOne(created);
		return created;
	}

	/// <summary>
	/// 主数据要下发的佩戴勋章**结构体**（GetMainDataRsp field 43 `on_medal`）。
	///
	/// 为什么必须回结构体而不是只回 id
	/// ------------------------------
	/// GetMainDataRsp 里有两个勋章字段：
	///   field 24 `on_medal_id` (uint32)   —— 旧字段
	///   field 43 `on_medal`    (Medal)    —— 客户端实际读的
	/// 只写 24 的话，「收藏 → 勋章」和个人资料卡都不会更新（点装备后能显示，
	/// 那是客户端自己的乐观 UI，一重登就回未佩戴）。同一个 `Medal` 结构还出现在
	/// PlayerDetailData / PlayerFriendBriefData / ChatworldPlayerInfo 里，
	/// 说明这一版客户端全面走结构体口径。
	///
	/// 未佩戴 / 徽章不存在 / 限时已过期一律回 null（字段不下发 = 没有佩戴）。
	/// </summary>
	public static Medal CurrentMedal(uint uid)
	{
		MedalEquipmentScheme row = FromUid(uid);
		if (row.OnMedalId == 0u)
		{
			return null;
		}

		MedalDataExcel excel = MedalData.GetInstance().FromId(row.OnMedalId);
		if (excel == null)
		{
			return null;
		}

		if (excel.IsLimited && row.LimitedEndTime <= Now)
		{
			return null;
		}

		return new Medal
		{
			Id = row.OnMedalId,
			EndTime = excel.IsLimited ? (uint)row.LimitedEndTime : 0u,
			ExtraParam = 0
		};
	}

	/// <summary>
	/// 主数据要下发的佩戴 id（旧字段 24）。与 <see cref="CurrentMedal"/> 同源，
	/// 两个字段一起写，避免客户端不同页面读到不一致的状态。
	/// </summary>
	public static uint Current(uint uid)
	{
		Medal medal = CurrentMedal(uid);
		return medal != null ? medal.Id : 0u;
	}

	/// <summary>
	/// 450 用的完整列表。end_time 口径必须和 <see cref="Current"/> 一致：
	/// 永久 = 0，限时 = 账号那条共用截止时间。
	/// </summary>
	public static Medal[] AllMedals(uint uid)
	{
		MedalEquipmentScheme row = FromUid(uid);
		uint endTime = row.LimitedEndTime > 0 ? (uint)row.LimitedEndTime : 0u;

		return MedalData.GetInstance().All
			.Where(x => x.MedalId > 0)
			.Select(x => new Medal
			{
				Id = (uint)x.MedalId,
				EndTime = x.IsLimited ? endTime : 0u,
				ExtraParam = 0
			})
			.ToArray();
	}

	/// <summary>该 id 是否在 MedalData 表里（本地服策略：表里有 = 已拥有）。</summary>
	public static bool IsOwned(uint medalId)
	{
		return medalId != 0u && MedalData.GetInstance().FromId(medalId) != null;
	}

	/// <summary>限时勋章是否已过期。</summary>
	public static bool IsExpired(uint uid, uint medalId)
	{
		MedalDataExcel excel = MedalData.GetInstance().FromId(medalId);
		if (excel == null || !excel.IsLimited)
		{
			return false;
		}

		return FromUid(uid).LimitedEndTime <= Now;
	}

	/// <summary>
	/// 佩戴 / 替换。返回 retcode，并通过 out 参数带回要回给客户端的 on_id / off_id。
	///
	/// 规则（照 MedalOpRsp 的语义）：
	///   * off_id 非 0 时必须等于当前佩戴 id，否则 OFF_ID_ERROR，且不动原选择
	///   * 同一枚重复佩戴视为成功，off 回 0（客户端不需要做替换动画）
	///   * on_id 不在表里 → NO_OWN；限时且过期 → EXPIRED
	///   * 成功时 on_id = 新 id，off_id = 被替换掉的旧 id
	/// </summary>
	public static MedalOpRsp.Retcode Wear(uint uid, uint onId, uint offId, out uint resultOn, out uint resultOff)
	{
		resultOn = 0u;
		resultOff = 0u;

		lock (_gate)
		{
			MedalEquipmentScheme row = FromUid(uid);
			uint current = row.OnMedalId;

			if (offId != 0u && offId != current)
			{
				return MedalOpRsp.Retcode.OffIdError;
			}

			if (onId == 0u)
			{
				return MedalOpRsp.Retcode.Fail;
			}

			// 重复佩戴同一枚：不写库，直接成功
			if (onId == current)
			{
				resultOn = onId;
				resultOff = 0u;
				return MedalOpRsp.Retcode.Succ;
			}

			if (!IsOwned(onId))
			{
				return MedalOpRsp.Retcode.NoOwn;
			}

			if (IsExpired(uid, onId))
			{
				return MedalOpRsp.Retcode.Expired;
			}

			row.OnMedalId = onId;
			row.Save();

			// 提交后回读，确认真的落盘了再回成功包
			if (FromUid(uid).OnMedalId != onId)
			{
				Packet.c.Log(string.Format("[Medal] 佩戴回读失败 uid={0} on={1}", uid, onId));
				return MedalOpRsp.Retcode.Fail;
			}

			resultOn = onId;
			resultOff = current;
			return MedalOpRsp.Retcode.Succ;
		}
	}

	/// <summary>
	/// 卸下。返回 retcode，on_id 恒 0，off_id 回原佩戴 id。
	/// 本来就没戴时按幂等成功处理（off 回 0），避免客户端重复点卸下报错。
	/// </summary>
	public static MedalOpRsp.Retcode Off(uint uid, uint offId, out uint resultOn, out uint resultOff)
	{
		resultOn = 0u;
		resultOff = 0u;

		lock (_gate)
		{
			MedalEquipmentScheme row = FromUid(uid);
			uint current = row.OnMedalId;

			if (current == 0u)
			{
				return MedalOpRsp.Retcode.Succ;
			}

			if (offId != 0u && offId != current)
			{
				return MedalOpRsp.Retcode.OffIdError;
			}

			row.OnMedalId = 0u;
			row.Save();

			if (FromUid(uid).OnMedalId != 0u)
			{
				Packet.c.Log(string.Format("[Medal] 卸下回读失败 uid={0}", uid));
				return MedalOpRsp.Retcode.Fail;
			}

			resultOff = current;
			return MedalOpRsp.Retcode.Succ;
		}
	}
}
