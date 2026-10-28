using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOtherDormDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 634
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit,
		[ProtoEnum(Name = "VISIT_FORBID")]
		VisitForbid,
		[ProtoEnum(Name = "TARGET_ERROR")]
		TargetError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__ShowRoom;

	private uint? __pbn__VisitAvatar;

	private uint? __pbn__BeLikedNum;

	private uint? __pbn__BeVisitNum;

	private bool? __pbn__IsReceiveLikeLimit;

	private bool? __pbn__IsHasLiked;

	private bool? __pbn__IsSendLikeLimit;

	private uint? __pbn__ComfortValue;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(3, Name = "house")]
	public DormHouse House { get; set; }

	[ProtoMember(4, Name = "show_room")]
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

	[ProtoMember(5, Name = "visit_avatar")]
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

	[ProtoMember(6, Name = "be_liked_num")]
	public uint BeLikedNum
	{
		get
		{
			return __pbn__BeLikedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BeLikedNum = value;
		}
	}

	[ProtoMember(7, Name = "record_list")]
	public List<DormVisitRecord> RecordLists { get; } = new List<DormVisitRecord>();


	[ProtoMember(8, Name = "be_visit_num")]
	public uint BeVisitNum
	{
		get
		{
			return __pbn__BeVisitNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BeVisitNum = value;
		}
	}

	[ProtoMember(9, Name = "is_receive_like_limit")]
	public bool IsReceiveLikeLimit
	{
		get
		{
			return __pbn__IsReceiveLikeLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__IsReceiveLikeLimit = value;
		}
	}

	[ProtoMember(10, Name = "is_has_liked")]
	public bool IsHasLiked
	{
		get
		{
			return __pbn__IsHasLiked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHasLiked = value;
		}
	}

	[ProtoMember(11, Name = "is_send_like_limit")]
	public bool IsSendLikeLimit
	{
		get
		{
			return __pbn__IsSendLikeLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSendLikeLimit = value;
		}
	}

	[ProtoMember(12, Name = "facility_list")]
	public List<DormFacility> FacilityLists { get; } = new List<DormFacility>();


	[ProtoMember(13, Name = "comfort_value")]
	public uint ComfortValue
	{
		get
		{
			return __pbn__ComfortValue.GetValueOrDefault();
		}
		set
		{
			__pbn__ComfortValue = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
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

	public bool ShouldSerializeBeLikedNum()
	{
		return __pbn__BeLikedNum.HasValue;
	}

	public void ResetBeLikedNum()
	{
		__pbn__BeLikedNum = null;
	}

	public bool ShouldSerializeBeVisitNum()
	{
		return __pbn__BeVisitNum.HasValue;
	}

	public void ResetBeVisitNum()
	{
		__pbn__BeVisitNum = null;
	}

	public bool ShouldSerializeIsReceiveLikeLimit()
	{
		return __pbn__IsReceiveLikeLimit.HasValue;
	}

	public void ResetIsReceiveLikeLimit()
	{
		__pbn__IsReceiveLikeLimit = null;
	}

	public bool ShouldSerializeIsHasLiked()
	{
		return __pbn__IsHasLiked.HasValue;
	}

	public void ResetIsHasLiked()
	{
		__pbn__IsHasLiked = null;
	}

	public bool ShouldSerializeIsSendLikeLimit()
	{
		return __pbn__IsSendLikeLimit.HasValue;
	}

	public void ResetIsSendLikeLimit()
	{
		__pbn__IsSendLikeLimit = null;
	}

	public bool ShouldSerializeComfortValue()
	{
		return __pbn__ComfortValue.HasValue;
	}

	public void ResetComfortValue()
	{
		__pbn__ComfortValue = null;
	}
}
