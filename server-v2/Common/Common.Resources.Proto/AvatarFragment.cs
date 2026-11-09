using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarFragment : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FragmentId;

	private uint? __pbn__Num;

	[ProtoMember(1, Name = "fragment_id")]
	public uint FragmentId
	{
		get
		{
			return __pbn__FragmentId.GetValueOrDefault();
		}
		set
		{
			__pbn__FragmentId = value;
		}
	}

	[ProtoMember(2, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFragmentId()
	{
		return __pbn__FragmentId.HasValue;
	}

	public void ResetFragmentId()
	{
		__pbn__FragmentId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}
}
