using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldEntity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__EntityType;

	private uint? __pbn__CampTag;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(2, Name = "entity_type")]
	public uint EntityType
	{
		get
		{
			return __pbn__EntityType.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityType = value;
		}
	}

	[ProtoMember(3, Name = "camp_tag")]
	public uint CampTag
	{
		get
		{
			return __pbn__CampTag.GetValueOrDefault();
		}
		set
		{
			__pbn__CampTag = value;
		}
	}

	[ProtoMember(4, Name = "position")]
	public ChatworldPosition Position { get; set; }

	[ProtoMember(16, Name = "player_info")]
	public ChatworldPlayerInfo PlayerInfo { get; set; }

	[ProtoMember(17, Name = "chatworld_item")]
	public ChatworldItem ChatworldItem { get; set; }

	[ProtoMember(18, Name = "chatworld_stance")]
	public ChatworldStance ChatworldStance { get; set; }

	[ProtoMember(19, Name = "chatworld_fish_spot")]
	public ChatworldFishSpot ChatworldFishSpot { get; set; }

	[ProtoMember(20, Name = "chatworld_question")]
	public ChatworldQuestion ChatworldQuestion { get; set; }

	[ProtoMember(21, Name = "chatworld_beast")]
	public ChatworldBeast ChatworldBeast { get; set; }

	[ProtoMember(22, Name = "chatworld_box")]
	public ChatworldBox ChatworldBox { get; set; }

	[ProtoMember(23, Name = "chatworld_wooden_boy")]
	public ChatworldWoodenBoy ChatworldWoodenBoy { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeEntityType()
	{
		return __pbn__EntityType.HasValue;
	}

	public void ResetEntityType()
	{
		__pbn__EntityType = null;
	}

	public bool ShouldSerializeCampTag()
	{
		return __pbn__CampTag.HasValue;
	}

	public void ResetCampTag()
	{
		__pbn__CampTag = null;
	}
}
