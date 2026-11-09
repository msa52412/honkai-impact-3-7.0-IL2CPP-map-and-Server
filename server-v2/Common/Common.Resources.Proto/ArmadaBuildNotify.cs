using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaBuildNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2678
	}

	private IExtension __pbn__extensionData;

	private ArmadaBuildNotifyType? __pbn__Type;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ArmadaBuildNotifyType.ArmadaBuildNotifyStart)]
	public ArmadaBuildNotifyType Type
	{
		get
		{
			return __pbn__Type ?? ArmadaBuildNotifyType.ArmadaBuildNotifyStart;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "armada_data")]
	public ArmadaData ArmadaData { get; set; }

	[ProtoMember(3, Name = "grid_list")]
	public List<ArmadaGrid> GridLists { get; } = new List<ArmadaGrid>();


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
