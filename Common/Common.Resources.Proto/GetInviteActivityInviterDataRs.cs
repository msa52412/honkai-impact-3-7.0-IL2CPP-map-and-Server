using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetInviteActivityInviterDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1691
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private string __pbn__MyInviteCode;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "my_invite_code")]
	[DefaultValue("")]
	public string MyInviteCode
	{
		get
		{
			return __pbn__MyInviteCode ?? "";
		}
		set
		{
			__pbn__MyInviteCode = value;
		}
	}

	[ProtoMember(3, Name = "inviter_activity_info_list")]
	public List<InviterActivity> InviterActivityInfoLists { get; } = new List<InviterActivity>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMyInviteCode()
	{
		return __pbn__MyInviteCode != null;
	}

	public void ResetMyInviteCode()
	{
		__pbn__MyInviteCode = null;
	}
}
