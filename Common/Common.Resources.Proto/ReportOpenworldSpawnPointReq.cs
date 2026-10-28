using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportOpenworldSpawnPointReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4486
	}

	private IExtension __pbn__extensionData;

	private string __pbn__PointInfo;

	private bool? __pbn__IsLdSpRecover;

	private uint? __pbn__MapId;

	[ProtoMember(1, Name = "point_info")]
	[DefaultValue("")]
	public string PointInfo
	{
		get
		{
			return __pbn__PointInfo ?? "";
		}
		set
		{
			__pbn__PointInfo = value;
		}
	}

	[ProtoMember(2, Name = "avatar_list")]
	public List<OpenworldStageAvatarInfo> AvatarLists { get; } = new List<OpenworldStageAvatarInfo>();


	[ProtoMember(3, Name = "is_ld_sp_recover")]
	public bool IsLdSpRecover
	{
		get
		{
			return __pbn__IsLdSpRecover.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLdSpRecover = value;
		}
	}

	[ProtoMember(4, Name = "sp_recover_avatar_list")]
	public uint[] SpRecoverAvatarLists { get; set; }

	[ProtoMember(5, Name = "map_id")]
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

	public bool ShouldSerializePointInfo()
	{
		return __pbn__PointInfo != null;
	}

	public void ResetPointInfo()
	{
		__pbn__PointInfo = null;
	}

	public bool ShouldSerializeIsLdSpRecover()
	{
		return __pbn__IsLdSpRecover.HasValue;
	}

	public void ResetIsLdSpRecover()
	{
		__pbn__IsLdSpRecover = null;
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
