using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpSkillPoint : IExtensible
{
	private IExtension __pbn__extensionData;

	private MpSkillType? __pbn__Type;

	private uint? __pbn__Value;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(MpSkillType.MpSkillNone)]
	public MpSkillType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "value")]
	public uint Value
	{
		get
		{
			return __pbn__Value.GetValueOrDefault();
		}
		set
		{
			__pbn__Value = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeValue()
	{
		return __pbn__Value.HasValue;
	}

	public void ResetValue()
	{
		__pbn__Value = null;
	}
}
