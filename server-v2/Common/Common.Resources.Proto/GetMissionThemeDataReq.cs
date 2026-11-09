using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMissionThemeDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4205
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ThemeId;

	private bool? __pbn__IsGetAll;

	[ProtoMember(1, Name = "theme_id")]
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

	[ProtoMember(2, Name = "is_get_all")]
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
