using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5600
	}

	private IExtension __pbn__extensionData;

	private ulong? __pbn__CurTimeMsU64;

	private bool? __pbn__IsAll;

	[ProtoMember(1, Name = "update_entity_list")]
	public List<QAvatarBattleEntity> UpdateEntityLists { get; } = new List<QAvatarBattleEntity>();


	[ProtoMember(3, Name = "cur_time_ms_u64")]
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

	[ProtoMember(4, Name = "delete_entity_list")]
	public List<QAvatarBattleDeleteEntity> DeleteEntityLists { get; } = new List<QAvatarBattleDeleteEntity>();


	[ProtoMember(5, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

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

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}
}
