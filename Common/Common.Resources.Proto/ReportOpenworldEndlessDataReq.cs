using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportOpenworldEndlessDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3434
	}

	private IExtension __pbn__extensionData;

	private OpenworldEndlessDataType? __pbn__Type;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(OpenworldEndlessDataType.OpenworldEndlessDataTypeInvaison)]
	public OpenworldEndlessDataType Type
	{
		get
		{
			return __pbn__Type ?? OpenworldEndlessDataType.OpenworldEndlessDataTypeInvaison;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "invasion")]
	public OpenworldEndlessReprotInvasion Invasion { get; set; }

	[ProtoMember(3, Name = "battle")]
	public OpenworldEndlessReprotBattle Battle { get; set; }

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
