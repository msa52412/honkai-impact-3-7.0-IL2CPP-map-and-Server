using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__ItemHp;

	[ProtoMember(1, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(2, Name = "item_hp")]
	public uint ItemHp
	{
		get
		{
			return __pbn__ItemHp.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemHp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeItemHp()
	{
		return __pbn__ItemHp.HasValue;
	}

	public void ResetItemHp()
	{
		__pbn__ItemHp = null;
	}
}
