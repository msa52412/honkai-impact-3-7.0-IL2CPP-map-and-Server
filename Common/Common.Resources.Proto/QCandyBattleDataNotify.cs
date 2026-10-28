using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6572
	}

	private IExtension __pbn__extensionData;

	private ulong? __pbn__CurTimeMsU64;

	[ProtoMember(1, Name = "cur_time_ms_u64")]
	public ulong CurTimeMsU64
	{
		get
		{
			return __pbn__CurTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__CurTimeMsU64 = value;
		}
	}

	[ProtoMember(2, Name = "update_entity_list")]
	public List<QCandyBattleEntity> UpdateEntityLists { get; } = new List<QCandyBattleEntity>();


	[ProtoMember(3, Name = "delete_entity_list")]
	public List<QCandyBattleEntity> DeleteEntityLists { get; } = new List<QCandyBattleEntity>();


	[ProtoMember(4, Name = "battle_info_list")]
	public List<QCandyBattleInfo> BattleInfoLists { get; } = new List<QCandyBattleInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurTimeMsU64()
	{
		return __pbn__CurTimeMsU64.HasValue;
	}

	public void ResetCurTimeMsU64()
	{
		__pbn__CurTimeMsU64 = null;
	}
}
