using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldStat : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__MaxNum;

	private uint? __pbn__FinishNum;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "max_num")]
	public uint MaxNum
	{
		get
		{
			return __pbn__MaxNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxNum = value;
		}
	}

	[ProtoMember(3, Name = "finish_num")]
	public uint FinishNum
	{
		get
		{
			return __pbn__FinishNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeMaxNum()
	{
		return __pbn__MaxNum.HasValue;
	}

	public void ResetMaxNum()
	{
		__pbn__MaxNum = null;
	}

	public bool ShouldSerializeFinishNum()
	{
		return __pbn__FinishNum.HasValue;
	}

	public void ResetFinishNum()
	{
		__pbn__FinishNum = null;
	}
}
