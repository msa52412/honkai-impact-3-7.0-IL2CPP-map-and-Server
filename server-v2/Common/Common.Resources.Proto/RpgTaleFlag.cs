using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleFlag : IExtensible
{
	private IExtension __pbn__extensionData;

	private RpgTaleFlagType? __pbn__Type;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(RpgTaleFlagType.RpgTaleFlagInvalid)]
	public RpgTaleFlagType Type
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

	[ProtoMember(2, Name = "param_list")]
	public uint[] ParamLists { get; set; }

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
}
