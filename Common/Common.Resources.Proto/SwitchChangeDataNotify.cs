using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchChangeDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3701
	}

	private IExtension __pbn__extensionData;

	private FeatureSwitchOpType? __pbn__OpType;

	[ProtoMember(1, Name = "op_type")]
	[DefaultValue(FeatureSwitchOpType.FeatureSwitchOpNone)]
	public FeatureSwitchOpType OpType
	{
		get
		{
			return __pbn__OpType.GetValueOrDefault();
		}
		set
		{
			__pbn__OpType = value;
		}
	}

	[ProtoMember(2, Name = "data_list")]
	public List<SwitchData> DataLists { get; } = new List<SwitchData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOpType()
	{
		return __pbn__OpType.HasValue;
	}

	public void ResetOpType()
	{
		__pbn__OpType = null;
	}
}
