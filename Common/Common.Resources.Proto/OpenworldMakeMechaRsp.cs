using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldMakeMechaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4517
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "LEVEL_UNLOCK")]
		LevelUnlock,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MechaId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "mecha_id")]
	public uint MechaId
	{
		get
		{
			return __pbn__MechaId.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMechaId()
	{
		return __pbn__MechaId.HasValue;
	}

	public void ResetMechaId()
	{
		__pbn__MechaId = null;
	}
}
