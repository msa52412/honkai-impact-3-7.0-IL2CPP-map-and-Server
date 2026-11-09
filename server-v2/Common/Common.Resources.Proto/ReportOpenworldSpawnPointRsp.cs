using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportOpenworldSpawnPointRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4487
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private string __pbn__PointInfo;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
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

	[ProtoMember(3, Name = "point_info")]
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

	public bool ShouldSerializePointInfo()
	{
		return __pbn__PointInfo != null;
	}

	public void ResetPointInfo()
	{
		__pbn__PointInfo = null;
	}
}
