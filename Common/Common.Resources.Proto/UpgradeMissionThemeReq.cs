using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpgradeMissionThemeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4207
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ThemeId;

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
}
