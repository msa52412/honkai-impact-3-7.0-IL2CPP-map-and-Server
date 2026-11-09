using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TimeLimitAddUpUpdateNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6738
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AddUpValue;

	[ProtoMember(1, Name = "add_up_value")]
	public uint AddUpValue
	{
		get
		{
			return __pbn__AddUpValue.GetValueOrDefault();
		}
		set
		{
			__pbn__AddUpValue = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddUpValue()
	{
		return __pbn__AddUpValue.HasValue;
	}

	public void ResetAddUpValue()
	{
		__pbn__AddUpValue = null;
	}
}
