using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetArmadaApplyReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2613
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ApplyLevel;

	private bool? __pbn__AutoPass;

	private uint? __pbn__AutoPassLevel;

	[ProtoMember(1, Name = "apply_level")]
	public uint ApplyLevel
	{
		get
		{
			return __pbn__ApplyLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ApplyLevel = value;
		}
	}

	[ProtoMember(2, Name = "auto_pass")]
	public bool AutoPass
	{
		get
		{
			return __pbn__AutoPass.GetValueOrDefault();
		}
		set
		{
			__pbn__AutoPass = value;
		}
	}

	[ProtoMember(3, Name = "auto_pass_level")]
	public uint AutoPassLevel
	{
		get
		{
			return __pbn__AutoPassLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AutoPassLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeApplyLevel()
	{
		return __pbn__ApplyLevel.HasValue;
	}

	public void ResetApplyLevel()
	{
		__pbn__ApplyLevel = null;
	}

	public bool ShouldSerializeAutoPass()
	{
		return __pbn__AutoPass.HasValue;
	}

	public void ResetAutoPass()
	{
		__pbn__AutoPass = null;
	}

	public bool ShouldSerializeAutoPassLevel()
	{
		return __pbn__AutoPassLevel.HasValue;
	}

	public void ResetAutoPassLevel()
	{
		__pbn__AutoPassLevel = null;
	}
}
