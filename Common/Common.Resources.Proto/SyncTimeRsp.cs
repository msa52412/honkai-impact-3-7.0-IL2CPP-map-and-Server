using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncTimeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 804
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

	private uint? __pbn__CurTime;

	private uint? __pbn__Seq;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_time")]
	public uint CurTime
	{
		get
		{
			return __pbn__CurTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CurTime = value;
		}
	}

	[ProtoMember(3, Name = "seq")]
	public uint Seq
	{
		get
		{
			return __pbn__Seq.GetValueOrDefault();
		}
		set
		{
			__pbn__Seq = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurTime()
	{
		return __pbn__CurTime.HasValue;
	}

	public void ResetCurTime()
	{
		__pbn__CurTime = null;
	}

	public bool ShouldSerializeSeq()
	{
		return __pbn__Seq.HasValue;
	}

	public void ResetSeq()
	{
		__pbn__Seq = null;
	}
}
