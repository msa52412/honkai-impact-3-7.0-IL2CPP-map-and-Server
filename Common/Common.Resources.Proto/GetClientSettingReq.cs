using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetClientSettingReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1270
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ClientSettingType;

	[ProtoMember(1, Name = "client_setting_type")]
	public uint ClientSettingType
	{
		get
		{
			return __pbn__ClientSettingType.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientSettingType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeClientSettingType()
	{
		return __pbn__ClientSettingType.HasValue;
	}

	public void ResetClientSettingType()
	{
		__pbn__ClientSettingType = null;
	}
}
