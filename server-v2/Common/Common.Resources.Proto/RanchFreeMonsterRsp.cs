using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchFreeMonsterRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5838
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
		[ProtoEnum(Name = "MONSTER_ALREADY_ASSIGNED")]
		MonsterAlreadyAssigned
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ReturnMaterialNum;

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

	[ProtoMember(4, Name = "return_material_num")]
	public uint ReturnMaterialNum
	{
		get
		{
			return __pbn__ReturnMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ReturnMaterialNum = value;
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

	public bool ShouldSerializeReturnMaterialNum()
	{
		return __pbn__ReturnMaterialNum.HasValue;
	}

	public void ResetReturnMaterialNum()
	{
		__pbn__ReturnMaterialNum = null;
	}
}
