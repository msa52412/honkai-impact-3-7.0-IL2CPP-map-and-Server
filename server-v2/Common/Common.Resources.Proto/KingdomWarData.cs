using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurPhase;

	private bool? __pbn__IsOver;

	private uint? __pbn__HasTakeMaxScore;

	private uint? __pbn__HasGetCurrencyNum;

	[ProtoMember(1, Name = "phase_list")]
	public List<KingdomWarPhase> PhaseLists { get; } = new List<KingdomWarPhase>();


	[ProtoMember(2, Name = "kingdom_list")]
	public List<KingdomWarKingdom> KingdomLists { get; } = new List<KingdomWarKingdom>();


	[ProtoMember(3, Name = "cur_phase")]
	public uint CurPhase
	{
		get
		{
			return __pbn__CurPhase.GetValueOrDefault();
		}
		set
		{
			__pbn__CurPhase = value;
		}
	}

	[ProtoMember(4, Name = "is_over")]
	public bool IsOver
	{
		get
		{
			return __pbn__IsOver.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOver = value;
		}
	}

	[ProtoMember(5, Name = "has_take_max_score")]
	public uint HasTakeMaxScore
	{
		get
		{
			return __pbn__HasTakeMaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeMaxScore = value;
		}
	}

	[ProtoMember(6, Name = "has_get_currency_num")]
	public uint HasGetCurrencyNum
	{
		get
		{
			return __pbn__HasGetCurrencyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HasGetCurrencyNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurPhase()
	{
		return __pbn__CurPhase.HasValue;
	}

	public void ResetCurPhase()
	{
		__pbn__CurPhase = null;
	}

	public bool ShouldSerializeIsOver()
	{
		return __pbn__IsOver.HasValue;
	}

	public void ResetIsOver()
	{
		__pbn__IsOver = null;
	}

	public bool ShouldSerializeHasTakeMaxScore()
	{
		return __pbn__HasTakeMaxScore.HasValue;
	}

	public void ResetHasTakeMaxScore()
	{
		__pbn__HasTakeMaxScore = null;
	}

	public bool ShouldSerializeHasGetCurrencyNum()
	{
		return __pbn__HasGetCurrencyNum.HasValue;
	}

	public void ResetHasGetCurrencyNum()
	{
		__pbn__HasGetCurrencyNum = null;
	}
}
