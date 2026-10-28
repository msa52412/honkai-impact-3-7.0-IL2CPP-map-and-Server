using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldWoodenBoyUpdateStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2483
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Score;

	private bool? __pbn__IsCaught;

	[ProtoMember(1, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(2, Name = "is_caught")]
	public bool IsCaught
	{
		get
		{
			return __pbn__IsCaught.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCaught = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeIsCaught()
	{
		return __pbn__IsCaught.HasValue;
	}

	public void ResetIsCaught()
	{
		__pbn__IsCaught = null;
	}
}
[ProtoContract]
public class ChatworldWoodenBoyUpdateStatusRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2484
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
