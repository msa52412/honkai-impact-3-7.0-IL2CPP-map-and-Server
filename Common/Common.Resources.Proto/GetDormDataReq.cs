using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDormDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 601
	}

	private IExtension __pbn__extensionData;

	private DormDataType? __pbn__DataType;

	[ProtoMember(1, Name = "data_type")]
	[DefaultValue(DormDataType.DormDataAll)]
	public DormDataType DataType
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
