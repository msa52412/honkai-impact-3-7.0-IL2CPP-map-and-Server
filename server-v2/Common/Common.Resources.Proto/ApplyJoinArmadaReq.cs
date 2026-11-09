using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ApplyJoinArmadaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2607
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ArmadaId;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "armada_id")]
	public uint ArmadaId
	{
		get
		{
			return __pbn__ArmadaId.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
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

	public bool ShouldSerializeArmadaId()
	{
		return __pbn__ArmadaId.HasValue;
	}

	public void ResetArmadaId()
	{
		__pbn__ArmadaId = null;
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
