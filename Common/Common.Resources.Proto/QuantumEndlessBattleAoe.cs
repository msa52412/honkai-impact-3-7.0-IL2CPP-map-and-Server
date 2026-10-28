using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QuantumEndlessBattleAoe : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AoeNum;

	private uint? __pbn__EvadeNum;

	[ProtoMember(1, Name = "aoe_num")]
	public uint AoeNum
	{
		get
		{
			return __pbn__AoeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AoeNum = value;
		}
	}

	[ProtoMember(2, Name = "evade_num")]
	public uint EvadeNum
	{
		get
		{
			return __pbn__EvadeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__EvadeNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAoeNum()
	{
		return __pbn__AoeNum.HasValue;
	}

	public void ResetAoeNum()
	{
		__pbn__AoeNum = null;
	}

	public bool ShouldSerializeEvadeNum()
	{
		return __pbn__EvadeNum.HasValue;
	}

	public void ResetEvadeNum()
	{
		__pbn__EvadeNum = null;
	}
}
