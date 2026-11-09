using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldStageInner : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ConditionType;

	private uint? __pbn__ConditionValue;

	[ProtoMember(1, Name = "condition_type")]
	public uint ConditionType
	{
		get
		{
			return __pbn__ConditionType.GetValueOrDefault();
		}
		set
		{
			__pbn__ConditionType = value;
		}
	}

	[ProtoMember(2, Name = "condition_value")]
	public uint ConditionValue
	{
		get
		{
			return __pbn__ConditionValue.GetValueOrDefault();
		}
		set
		{
			__pbn__ConditionValue = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeConditionType()
	{
		return __pbn__ConditionType.HasValue;
	}

	public void ResetConditionType()
	{
		__pbn__ConditionType = null;
	}

	public bool ShouldSerializeConditionValue()
	{
		return __pbn__ConditionValue.HasValue;
	}

	public void ResetConditionValue()
	{
		__pbn__ConditionValue = null;
	}
}
