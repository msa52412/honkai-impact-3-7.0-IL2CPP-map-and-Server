using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetEndlessStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3432
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	private bool? __pbn__IsFinishFront;

	[ProtoMember(1, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(2, Name = "is_finish_front")]
	public bool IsFinishFront
	{
		get
		{
			return __pbn__IsFinishFront.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinishFront = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeIsFinishFront()
	{
		return __pbn__IsFinishFront.HasValue;
	}

	public void ResetIsFinishFront()
	{
		__pbn__IsFinishFront = null;
	}
}
