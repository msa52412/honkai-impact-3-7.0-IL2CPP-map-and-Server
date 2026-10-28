using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportReplayFrameRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5405
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

	private string __pbn__Key;

	private uint? __pbn__Seq;

	private uint? __pbn__Count;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "key")]
	[DefaultValue("")]
	public string Key
	{
		get
		{
			return __pbn__Key ?? "";
		}
		set
		{
			__pbn__Key = value;
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

	[ProtoMember(4, Name = "count")]
	public uint Count
	{
		get
		{
			return __pbn__Count.GetValueOrDefault();
		}
		set
		{
			__pbn__Count = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKey()
	{
		return __pbn__Key != null;
	}

	public void ResetKey()
	{
		__pbn__Key = null;
	}

	public bool ShouldSerializeSeq()
	{
		return __pbn__Seq.HasValue;
	}

	public void ResetSeq()
	{
		__pbn__Seq = null;
	}

	public bool ShouldSerializeCount()
	{
		return __pbn__Count.HasValue;
	}

	public void ResetCount()
	{
		__pbn__Count = null;
	}
}
