using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayFrame : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Seq;

	private uint? __pbn__Count;

	private byte[] __pbn__Frame;

	[ProtoMember(1, Name = "seq")]
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

	[ProtoMember(2, Name = "count")]
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

	[ProtoMember(3, Name = "frame")]
	public byte[] Frame
	{
		get
		{
			return __pbn__Frame;
		}
		set
		{
			__pbn__Frame = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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

	public bool ShouldSerializeFrame()
	{
		return __pbn__Frame != null;
	}

	public void ResetFrame()
	{
		__pbn__Frame = null;
	}
}
