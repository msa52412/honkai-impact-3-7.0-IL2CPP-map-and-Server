using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExchangePurpleJadeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1367
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HcoinNum;

	private uint? __pbn__McoinNum;

	[ProtoMember(1, Name = "hcoin_num")]
	public uint HcoinNum
	{
		get
		{
			return __pbn__HcoinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HcoinNum = value;
		}
	}

	[ProtoMember(2, Name = "mcoin_num")]
	public uint McoinNum
	{
		get
		{
			return __pbn__McoinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__McoinNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHcoinNum()
	{
		return __pbn__HcoinNum.HasValue;
	}

	public void ResetHcoinNum()
	{
		__pbn__HcoinNum = null;
	}

	public bool ShouldSerializeMcoinNum()
	{
		return __pbn__McoinNum.HasValue;
	}

	public void ResetMcoinNum()
	{
		__pbn__McoinNum = null;
	}
}
