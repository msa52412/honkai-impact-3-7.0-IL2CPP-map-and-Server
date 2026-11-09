using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ItemUseSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UseId;

	[ProtoMember(1, Name = "use_id")]
	public uint UseId
	{
		get
		{
			return __pbn__UseId.GetValueOrDefault();
		}
		set
		{
			__pbn__UseId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUseId()
	{
		return __pbn__UseId.HasValue;
	}

	public void ResetUseId()
	{
		__pbn__UseId = null;
	}
}
