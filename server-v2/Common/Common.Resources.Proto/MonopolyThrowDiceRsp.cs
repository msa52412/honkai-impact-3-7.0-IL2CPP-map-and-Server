using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyThrowDiceRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3853
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "WAIT_SELECT_PATH")]
		WaitSelectPath,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__DiceNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "area_id")]
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

	[ProtoMember(3, Name = "dice_num")]
	public uint DiceNum
	{
		get
		{
			return __pbn__DiceNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DiceNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeDiceNum()
	{
		return __pbn__DiceNum.HasValue;
	}

	public void ResetDiceNum()
	{
		__pbn__DiceNum = null;
	}
}
