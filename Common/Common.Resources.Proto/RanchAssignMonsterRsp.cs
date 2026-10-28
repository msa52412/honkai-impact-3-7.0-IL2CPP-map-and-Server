using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchAssignMonsterRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5840
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MONSTER_NOT_EXIST")]
		MonsterNotExist,
		[ProtoEnum(Name = "UNIQUE_ID_REPEATED")]
		UniqueIdRepeated,
		[ProtoEnum(Name = "AREA_NOT_EXIST")]
		AreaNotExist,
		[ProtoEnum(Name = "MONSTER_ALREADY_ASSIGNED")]
		MonsterAlreadyAssigned,
		[ProtoEnum(Name = "SLOT_NOT_ENOUGH")]
		SlotNotEnough
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__AreaId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "unique_id_list")]
	public uint[] UniqueIdLists { get; set; }

	[ProtoMember(4, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}
}
