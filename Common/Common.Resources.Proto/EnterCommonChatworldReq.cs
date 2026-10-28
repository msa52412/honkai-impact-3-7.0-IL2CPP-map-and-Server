using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EnterCommonChatworldReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2400
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__SceneId;

	private uint? __pbn__LastChatworldId;

	private uint? __pbn__CampTag;

	private uint? __pbn__LastSceneId;

	[ProtoMember(1, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(2, Name = "player_info")]
	public ChatworldPlayerInfo PlayerInfo { get; set; }

	[ProtoMember(3, Name = "scene_id")]
	public uint SceneId
	{
		get
		{
			return __pbn__SceneId.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneId = value;
		}
	}

	[ProtoMember(4, Name = "scene_data")]
	public ChatworldPlayerScene SceneData { get; set; }

	[ProtoMember(5, Name = "last_chatworld_id")]
	public uint LastChatworldId
	{
		get
		{
			return __pbn__LastChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastChatworldId = value;
		}
	}

	[ProtoMember(6, Name = "camp_tag")]
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

	[ProtoMember(7, Name = "last_scene_id")]
	public uint LastSceneId
	{
		get
		{
			return __pbn__LastSceneId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSceneId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeSceneId()
	{
		return __pbn__SceneId.HasValue;
	}

	public void ResetSceneId()
	{
		__pbn__SceneId = null;
	}

	public bool ShouldSerializeLastChatworldId()
	{
		return __pbn__LastChatworldId.HasValue;
	}

	public void ResetLastChatworldId()
	{
		__pbn__LastChatworldId = null;
	}

	public bool ShouldSerializeCampTag()
	{
		return __pbn__CampTag.HasValue;
	}

	public void ResetCampTag()
	{
		__pbn__CampTag = null;
	}

	public bool ShouldSerializeLastSceneId()
	{
		return __pbn__LastSceneId.HasValue;
	}

	public void ResetLastSceneId()
	{
		__pbn__LastSceneId = null;
	}
}
