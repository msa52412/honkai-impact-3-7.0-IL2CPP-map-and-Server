using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2601
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
