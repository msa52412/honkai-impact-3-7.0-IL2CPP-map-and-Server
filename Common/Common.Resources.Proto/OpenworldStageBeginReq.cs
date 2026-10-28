using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldStageBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4462
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__MapId;

	private OpenworldStageType? __pbn__OpenworldStageType;

	private uint? __pbn__StageId;

	private uint? __pbn__SupportAvatarId;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(2, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
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

	[ProtoMember(4, Name = "elf_id_list")]
	public uint[] ElfIdLists { get; set; }

	[ProtoMember(5, Name = "openworld_stage_type")]
	[DefaultValue(OpenworldStageType.OpenworldStageTypeNone)]
	public OpenworldStageType OpenworldStageType
	{
		get
		{
			return __pbn__OpenworldStageType.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenworldStageType = value;
		}
	}

	[ProtoMember(6, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(7, Name = "support_avatar_id")]
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

	[ProtoMember(8, Name = "chapter_ow_info")]
	public OpenworldChapterStageBeginInfo ChapterOwInfo { get; set; }

	[ProtoMember(9, Name = "report_ext")]
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

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeOpenworldStageType()
	{
		return __pbn__OpenworldStageType.HasValue;
	}

	public void ResetOpenworldStageType()
	{
		__pbn__OpenworldStageType = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
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
