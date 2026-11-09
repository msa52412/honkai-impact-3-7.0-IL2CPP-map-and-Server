using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetFriendRemarkRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 973
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_FRIEND")]
		NotFriend,
		[ProtoEnum(Name = "FORMAT_ERROR")]
		FormatError,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "SAME_REMARK")]
		SameRemark,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "IN_AUDIT")]
		InAudit,
		[ProtoEnum(Name = "IN_CD")]
		InCd
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Remark;

	private uint? __pbn__SetFriendRemarkCdEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "uid")]
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

	[ProtoMember(3, Name = "remark")]
	[DefaultValue("")]
	public string Remark
	{
		get
		{
			return __pbn__Remark ?? "";
		}
		set
		{
			__pbn__Remark = value;
		}
	}

	[ProtoMember(4, Name = "set_friend_remark_cd_end_time")]
	public uint SetFriendRemarkCdEndTime
	{
		get
		{
			return __pbn__SetFriendRemarkCdEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SetFriendRemarkCdEndTime = value;
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

	public bool ShouldSerializeRemark()
	{
		return __pbn__Remark != null;
	}

	public void ResetRemark()
	{
		__pbn__Remark = null;
	}

	public bool ShouldSerializeSetFriendRemarkCdEndTime()
	{
		return __pbn__SetFriendRemarkCdEndTime.HasValue;
	}

	public void ResetSetFriendRemarkCdEndTime()
	{
		__pbn__SetFriendRemarkCdEndTime = null;
	}
}
