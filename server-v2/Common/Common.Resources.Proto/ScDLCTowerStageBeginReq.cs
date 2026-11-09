using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCTowerStageBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6334
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SupportAvatarId;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(2, Name = "support_avatar_id")]
	public uint SupportAvatarId
	{
		get
		{
			return __pbn__SupportAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportAvatarId = value;
		}
	}

	[ProtoMember(3, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSupportAvatarId()
	{
		return __pbn__SupportAvatarId.HasValue;
	}

	public void ResetSupportAvatarId()
	{
		__pbn__SupportAvatarId = null;
	}

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
