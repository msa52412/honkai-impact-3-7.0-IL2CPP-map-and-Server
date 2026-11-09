using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportOpenworldAreaInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4538
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__AreaId;

	private string __pbn__AreaInfo;

	[ProtoMember(1, Name = "map_id")]
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

	[ProtoMember(3, Name = "area_info")]
	[DefaultValue("")]
	public string AreaInfo
	{
		get
		{
			return __pbn__AreaInfo ?? "";
		}
		set
		{
			__pbn__AreaInfo = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeAreaInfo()
	{
		return __pbn__AreaInfo != null;
	}

	public void ResetAreaInfo()
	{
		__pbn__AreaInfo = null;
	}
}
