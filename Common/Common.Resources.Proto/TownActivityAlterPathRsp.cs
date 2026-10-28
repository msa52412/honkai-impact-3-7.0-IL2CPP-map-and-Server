using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityAlterPathRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6479
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_ALTER")]
		NoAlter,
		[ProtoEnum(Name = "NOT_MEET_CONDITION")]
		NotMeetCondition
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Position;

	private uint? __pbn__NextPosition;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(3, Name = "position")]
	public uint Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(4, Name = "next_position")]
	public uint NextPosition
	{
		get
		{
			return __pbn__NextPosition.GetValueOrDefault();
		}
		set
		{
			__pbn__NextPosition = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeNextPosition()
	{
		return __pbn__NextPosition.HasValue;
	}

	public void ResetNextPosition()
	{
		__pbn__NextPosition = null;
	}
}
