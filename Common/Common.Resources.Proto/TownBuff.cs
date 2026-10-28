using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BuffId;

	private uint? __pbn__BuffNum;

	private uint? __pbn__LeftDurationNum;

	[ProtoMember(1, Name = "buff_id")]
	public uint BuffId
	{
		get
		{
			return __pbn__BuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffId = value;
		}
	}

	[ProtoMember(2, Name = "buff_num")]
	public uint BuffNum
	{
		get
		{
			return __pbn__BuffNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffNum = value;
		}
	}

	[ProtoMember(3, Name = "left_duration_num")]
	public uint LeftDurationNum
	{
		get
		{
			return __pbn__LeftDurationNum.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftDurationNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuffId()
	{
		return __pbn__BuffId.HasValue;
	}

	public void ResetBuffId()
	{
		__pbn__BuffId = null;
	}

	public bool ShouldSerializeBuffNum()
	{
		return __pbn__BuffNum.HasValue;
	}

	public void ResetBuffNum()
	{
		__pbn__BuffNum = null;
	}

	public bool ShouldSerializeLeftDurationNum()
	{
		return __pbn__LeftDurationNum.HasValue;
	}

	public void ResetLeftDurationNum()
	{
		__pbn__LeftDurationNum = null;
	}
}
