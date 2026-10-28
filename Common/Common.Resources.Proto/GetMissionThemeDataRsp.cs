using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMissionThemeDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4206
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ThemeId;

	private bool? __pbn__IsGetAll;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "theme_id")]
	public uint ThemeId
	{
		get
		{
			return __pbn__ThemeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ThemeId = value;
		}
	}

	[ProtoMember(3, Name = "is_get_all")]
	public bool IsGetAll
	{
		get
		{
			return __pbn__IsGetAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGetAll = value;
		}
	}

	[ProtoMember(4, Name = "mission_theme_list")]
	public List<MissionThemeData> MissionThemeLists { get; } = new List<MissionThemeData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeThemeId()
	{
		return __pbn__ThemeId.HasValue;
	}

	public void ResetThemeId()
	{
		__pbn__ThemeId = null;
	}

	public bool ShouldSerializeIsGetAll()
	{
		return __pbn__IsGetAll.HasValue;
	}

	public void ResetIsGetAll()
	{
		__pbn__IsGetAll = null;
	}
}
