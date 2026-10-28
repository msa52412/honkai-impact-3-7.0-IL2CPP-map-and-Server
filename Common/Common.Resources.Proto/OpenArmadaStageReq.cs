using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenArmadaStageReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2799
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HardLevel;

	[ProtoMember(1, Name = "hard_level")]
	public uint HardLevel
	{
		get
		{
			return __pbn__HardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HardLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
	}
}
