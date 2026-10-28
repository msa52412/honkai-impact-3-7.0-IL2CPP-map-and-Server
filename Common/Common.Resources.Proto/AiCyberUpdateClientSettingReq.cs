using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberUpdateClientSettingReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6660
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HyperionId;

	private uint? __pbn__MainUiId;

	[ProtoMember(1, Name = "hyperion_id")]
	public uint HyperionId
	{
		get
		{
			return __pbn__HyperionId.GetValueOrDefault();
		}
		set
		{
			__pbn__HyperionId = value;
		}
	}

	[ProtoMember(2, Name = "main_ui_id")]
	public uint MainUiId
	{
		get
		{
			return __pbn__MainUiId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainUiId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHyperionId()
	{
		return __pbn__HyperionId.HasValue;
	}

	public void ResetHyperionId()
	{
		__pbn__HyperionId = null;
	}

	public bool ShouldSerializeMainUiId()
	{
		return __pbn__MainUiId.HasValue;
	}

	public void ResetMainUiId()
	{
		__pbn__MainUiId = null;
	}
}
