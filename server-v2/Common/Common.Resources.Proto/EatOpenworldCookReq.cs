using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EatOpenworldCookReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4492
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CookId;

	private uint? __pbn__AvatarId;

	private uint? __pbn__MapId;

	[ProtoMember(1, Name = "cook_id")]
	public uint CookId
	{
		get
		{
			return __pbn__CookId.GetValueOrDefault();
		}
		set
		{
			__pbn__CookId = value;
		}
	}

	[ProtoMember(2, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(3, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCookId()
	{
		return __pbn__CookId.HasValue;
	}

	public void ResetCookId()
	{
		__pbn__CookId = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
