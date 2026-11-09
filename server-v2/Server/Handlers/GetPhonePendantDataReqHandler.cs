using System;
using System.IO;
using System.Linq;
using Common.Utils.ExcelReader;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 「个性装饰」（手机挂件 / PhonePendant，PhoneStrapDialogContext 那个对话框）。
///
/// 客户端解锁判定（反编译 MoleMole.MonoPhoneStrap.RefreshStatus，0x1829A7C10）：
///     _isUnlock = _data.PHKHBEHHNOL;               // 也就是 HGDFACBMDIJ + 0xA8
///     Find(Lock).SetActive(_isUnlock);             // 锁图标
///     Find(LockedMask).SetActive(!_isUnlock);
/// 这个字节的**唯一**服务端来源是 GetPhonePendantDataRsp.phone_pendant_list：
///     MonoPhonePendantModule.OnGetPhonePendantDataRsp (0x18056D7B0)
///       → 逐项 PhonePendantItem::Init(endTime)  (0x18192A5A0)
///            TimeType(配置)==1 → +0xA8 = 1
///            否则             → +0xA8 = (endTime &gt;= now)
/// 模块初始化（0x18056D5D0）只按配置表 new 出 48 个 item、该字节是 0，
/// 所以服务端不回这条协议时整页挂件全带锁（日志里 `PhonePendantDataReq not handled` 刷 182 条）。
///
/// 这里把配置表里的 48 个 id 全部列进列表 = 全部解锁。
/// 运行期无需开关；客户端配置表里没有的 id 会被它自己丢弃（字典 ContainsKey 为假），多给无害。
/// </summary>
[PacketCmdId(CmdId.GetPhonePendantDataReq)]
internal class GetPhonePendantDataReqHandler : IPacketHandler
{
	/// <summary>2099-12-31，仅作兜底：TimeType 非 1 的项客户端会用 end_time &gt;= now 判定。</summary>
	private const uint FarFuture = 4102415999u;

	public void Handle(Session session, Packet packet)
	{
		GetPhonePendantDataRsp rsp = new GetPhonePendantDataRsp
		{
			retcode = GetPhonePendantDataRsp.Retcode.Succ,
			IsAll = true
		};

		uint[] ids = Ids();
		foreach (uint id in ids)
		{
			rsp.PhonePendantLists.Add(new PhonePendant
			{
				Id = id,
				EndTime = FarFuture
			});
		}

		Packet.c.Log(string.Format("[PhonePendant] 下发 {0} 个挂件（全部解锁）", ids.Length));
		session.Send(Packet.FromProto(rsp, CmdId.GetPhonePendantDataRsp));
	}

	/// <summary>配置表 id；运行目录放 phone_pendant.txt（逗号或区间）可手工指定。</summary>
	private static uint[] Ids()
	{
		uint[] configIds = PhonePendantData.GetInstance().AllIds();
		try
		{
			string path = Path.Combine(AppContext.BaseDirectory, "phone_pendant.txt");
			if (!File.Exists(path))
			{
				return configIds;
			}

			uint[] custom = File.ReadAllText(path)
				.Split(new[] { ',', '，', ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.Trim())
				.Where(x => x.Length > 0)
				.Select(x =>
				{
					string[] pair = x.Split('-');
					if (pair.Length == 2 && uint.TryParse(pair[0], out uint from) && uint.TryParse(pair[1], out uint to) && to >= from)
					{
						return (from, to);
					}
					uint one = uint.TryParse(x, out uint id) ? id : 0u;
					return (one, one);
				})
				.Where(x => x.Item1 > 0)
				.SelectMany(x => Enumerable.Range(0, (int)(x.Item2 - x.Item1 + 1)).Select(i => x.Item1 + (uint)i))
				.Distinct()
				.ToArray();

			return custom.Length > 0 ? custom : configIds;
		}
		catch
		{
			return configIds;
		}
	}
}
