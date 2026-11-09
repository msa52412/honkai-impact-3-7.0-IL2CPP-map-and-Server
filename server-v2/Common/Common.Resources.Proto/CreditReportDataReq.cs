using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreditReportDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6000
	}

	private IExtension __pbn__extensionData;

	private CreditType? __pbn__Type;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(CreditType.CreditNone)]
	public CreditType Type
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

	[ProtoMember(2, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


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
