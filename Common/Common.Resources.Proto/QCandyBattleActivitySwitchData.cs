using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleActivitySwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SwitchType;

	[ProtoMember(1, Name = "switch_type")]
	public uint SwitchType
	{
		get
		{
			return __pbn__SwitchType.GetValueOrDefault();
		}
		set
		{
			__pbn__SwitchType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSwitchType()
	{
		return __pbn__SwitchType.HasValue;
	}

	public void ResetSwitchType()
	{
		__pbn__SwitchType = null;
	}
}
