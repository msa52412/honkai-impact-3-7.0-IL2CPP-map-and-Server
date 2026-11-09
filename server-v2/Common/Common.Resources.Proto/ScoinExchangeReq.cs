using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScoinExchangeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 14
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PurpleJadeNum;

	[ProtoMember(1, Name = "purple_jade_num")]
	public uint PurpleJadeNum
	{
		get
		{
			return __pbn__PurpleJadeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PurpleJadeNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePurpleJadeNum()
	{
		return __pbn__PurpleJadeNum.HasValue;
	}

	public void ResetPurpleJadeNum()
	{
		__pbn__PurpleJadeNum = null;
	}
}
