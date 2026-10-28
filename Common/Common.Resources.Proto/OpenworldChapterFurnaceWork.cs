using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterFurnaceWork : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__FormulaId;

	private OpenworldChapterFurnaceWorkStatus? __pbn__Status;

	private uint? __pbn__FinishTime;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(2, Name = "formula_id")]
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

	[ProtoMember(5, Name = "status")]
	[DefaultValue(OpenworldChapterFurnaceWorkStatus.OpenworldChapterFurnaceWorkInvalid)]
	public OpenworldChapterFurnaceWorkStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(6, Name = "finish_time")]
	public uint FinishTime
	{
		get
		{
			return __pbn__FinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeFormulaId()
	{
		return __pbn__FormulaId.HasValue;
	}

	public void ResetFormulaId()
	{
		__pbn__FormulaId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeFinishTime()
	{
		return __pbn__FinishTime.HasValue;
	}

	public void ResetFinishTime()
	{
		__pbn__FinishTime = null;
	}
}
