using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterFurnaceFormulaUseHistory : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FormulaId;

	private uint? __pbn__UseTimes;

	[ProtoMember(1, Name = "formula_id")]
	public uint FormulaId
	{
		get
		{
			return __pbn__FormulaId.GetValueOrDefault();
		}
		set
		{
			__pbn__FormulaId = value;
		}
	}

	[ProtoMember(2, Name = "use_times")]
	public uint UseTimes
	{
		get
		{
			return __pbn__UseTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__UseTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFormulaId()
	{
		return __pbn__FormulaId.HasValue;
	}

	public void ResetFormulaId()
	{
		__pbn__FormulaId = null;
	}

	public bool ShouldSerializeUseTimes()
	{
		return __pbn__UseTimes.HasValue;
	}

	public void ResetUseTimes()
	{
		__pbn__UseTimes = null;
	}
}
