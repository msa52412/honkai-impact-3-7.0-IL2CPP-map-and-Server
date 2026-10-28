using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BuffId;

	private uint? __pbn__BuffType;

	private uint? __pbn__BuffParaInt;

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

	[ProtoMember(2, Name = "buff_type")]
	public uint BuffType
	{
		get
		{
			return __pbn__BuffType.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffType = value;
		}
	}

	[ProtoMember(3, Name = "buff_para_int")]
	public uint BuffParaInt
	{
		get
		{
			return __pbn__BuffParaInt.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffParaInt = value;
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

	public bool ShouldSerializeBuffType()
	{
		return __pbn__BuffType.HasValue;
	}

	public void ResetBuffType()
	{
		__pbn__BuffType = null;
	}

	public bool ShouldSerializeBuffParaInt()
	{
		return __pbn__BuffParaInt.HasValue;
	}

	public void ResetBuffParaInt()
	{
		__pbn__BuffParaInt = null;
	}
}
