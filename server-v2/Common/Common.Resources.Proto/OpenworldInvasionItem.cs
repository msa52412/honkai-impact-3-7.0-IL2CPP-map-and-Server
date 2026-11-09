using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldInvasionItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__InvasionId;

	[ProtoMember(1, Name = "invasion_id")]
	public uint InvasionId
	{
		get
		{
			return __pbn__InvasionId.GetValueOrDefault();
		}
		set
		{
			__pbn__InvasionId = value;
		}
	}

	[ProtoMember(2, Name = "avatar_list")]
	public uint[] AvatarLists { get; set; }

	[ProtoMember(3, Name = "buff_list")]
	public uint[] BuffLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeInvasionId()
	{
		return __pbn__InvasionId.HasValue;
	}

	public void ResetInvasionId()
	{
		__pbn__InvasionId = null;
	}
}
