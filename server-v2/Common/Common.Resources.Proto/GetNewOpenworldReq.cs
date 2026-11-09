using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetNewOpenworldReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4496
	}

	private IExtension __pbn__extensionData;

	private OpenworldDataType? __pbn__DataType;

	[ProtoMember(1, Name = "data_type")]
	[DefaultValue(OpenworldDataType.OpenworldDataAll)]
	public OpenworldDataType DataType
	{
		get
		{
			return __pbn__DataType.GetValueOrDefault();
		}
		set
		{
			__pbn__DataType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDataType()
	{
		return __pbn__DataType.HasValue;
	}

	public void ResetDataType()
	{
		__pbn__DataType = null;
	}
}
