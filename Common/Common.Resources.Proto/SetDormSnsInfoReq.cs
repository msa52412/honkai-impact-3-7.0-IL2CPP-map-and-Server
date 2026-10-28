using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetDormSnsInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 635
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ShowHouse;

	private uint? __pbn__ShowRoom;

	private uint? __pbn__VisitAvatar;

	private bool? __pbn__IsAllowVisit;

	[ProtoMember(1, Name = "show_house")]
	public uint ShowHouse
	{
		get
		{
			return __pbn__ShowHouse.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowHouse = value;
		}
	}

	[ProtoMember(2, Name = "show_room")]
	public uint ShowRoom
	{
		get
		{
			return __pbn__ShowRoom.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowRoom = value;
		}
	}

	[ProtoMember(3, Name = "visit_avatar")]
	public uint VisitAvatar
	{
		get
		{
			return __pbn__VisitAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitAvatar = value;
		}
	}

	[ProtoMember(4, Name = "is_allow_visit")]
	public bool IsAllowVisit
	{
		get
		{
			return __pbn__IsAllowVisit.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllowVisit = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShowHouse()
	{
		return __pbn__ShowHouse.HasValue;
	}

	public void ResetShowHouse()
	{
		__pbn__ShowHouse = null;
	}

	public bool ShouldSerializeShowRoom()
	{
		return __pbn__ShowRoom.HasValue;
	}

	public void ResetShowRoom()
	{
		__pbn__ShowRoom = null;
	}

	public bool ShouldSerializeVisitAvatar()
	{
		return __pbn__VisitAvatar.HasValue;
	}

	public void ResetVisitAvatar()
	{
		__pbn__VisitAvatar = null;
	}

	public bool ShouldSerializeIsAllowVisit()
	{
		return __pbn__IsAllowVisit.HasValue;
	}

	public void ResetIsAllowVisit()
	{
		__pbn__IsAllowVisit = null;
	}
}
