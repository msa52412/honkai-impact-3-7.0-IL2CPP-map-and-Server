using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalWarMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private uint? __pbn__HeadAvatarId;

	private uint? __pbn__FrameId;

	private uint? __pbn__DressId;

	private uint? __pbn__CustomHeadId;

	private uint? __pbn__GlobalWarPointId;

	private uint? __pbn__GlobalWarAddCurrency;

	private uint? __pbn__GlobalWarAddTime;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(4, Name = "head_avatar_id")]
	public uint HeadAvatarId
	{
		get
		{
			return __pbn__HeadAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__HeadAvatarId = value;
		}
	}

	[ProtoMember(5, Name = "frame_id")]
	public uint FrameId
	{
		get
		{
			return __pbn__FrameId.GetValueOrDefault();
		}
		set
		{
			__pbn__FrameId = value;
		}
	}

	[ProtoMember(6, Name = "dress_id")]
	public uint DressId
	{
		get
		{
			return __pbn__DressId.GetValueOrDefault();
		}
		set
		{
			__pbn__DressId = value;
		}
	}

	[ProtoMember(7, Name = "custom_head_id")]
	public uint CustomHeadId
	{
		get
		{
			return __pbn__CustomHeadId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomHeadId = value;
		}
	}

	[ProtoMember(8, Name = "global_war_point_id")]
	public uint GlobalWarPointId
	{
		get
		{
			return __pbn__GlobalWarPointId.GetValueOrDefault();
		}
		set
		{
			__pbn__GlobalWarPointId = value;
		}
	}

	[ProtoMember(9, Name = "global_war_add_currency")]
	public uint GlobalWarAddCurrency
	{
		get
		{
			return __pbn__GlobalWarAddCurrency.GetValueOrDefault();
		}
		set
		{
			__pbn__GlobalWarAddCurrency = value;
		}
	}

	[ProtoMember(10, Name = "global_war_add_time")]
	public uint GlobalWarAddTime
	{
		get
		{
			return __pbn__GlobalWarAddTime.GetValueOrDefault();
		}
		set
		{
			__pbn__GlobalWarAddTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeHeadAvatarId()
	{
		return __pbn__HeadAvatarId.HasValue;
	}

	public void ResetHeadAvatarId()
	{
		__pbn__HeadAvatarId = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeGlobalWarPointId()
	{
		return __pbn__GlobalWarPointId.HasValue;
	}

	public void ResetGlobalWarPointId()
	{
		__pbn__GlobalWarPointId = null;
	}

	public bool ShouldSerializeGlobalWarAddCurrency()
	{
		return __pbn__GlobalWarAddCurrency.HasValue;
	}

	public void ResetGlobalWarAddCurrency()
	{
		__pbn__GlobalWarAddCurrency = null;
	}

	public bool ShouldSerializeGlobalWarAddTime()
	{
		return __pbn__GlobalWarAddTime.HasValue;
	}

	public void ResetGlobalWarAddTime()
	{
		__pbn__GlobalWarAddTime = null;
	}
}
