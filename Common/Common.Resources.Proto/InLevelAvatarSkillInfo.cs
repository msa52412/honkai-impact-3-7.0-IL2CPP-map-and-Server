using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelAvatarSkillInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__SkillName;

	private float? __pbn__Cd;

	private float? __pbn__CostSp;

	private float? __pbn__NeedSp;

	private float? __pbn__CdTimer;

	private uint? __pbn__MaxChargesCount;

	private uint? __pbn__ChargesCounter;

	private bool? __pbn__CanHold;

	private string __pbn__InstantTriggerAnimEventId;

	[ProtoMember(1, Name = "skill_name")]
	[DefaultValue("")]
	public string SkillName
	{
		get
		{
			return __pbn__SkillName ?? "";
		}
		set
		{
			__pbn__SkillName = value;
		}
	}

	[ProtoMember(2, Name = "cd")]
	public float Cd
	{
		get
		{
			return __pbn__Cd.GetValueOrDefault();
		}
		set
		{
			__pbn__Cd = value;
		}
	}

	[ProtoMember(3, Name = "cost_sp")]
	public float CostSp
	{
		get
		{
			return __pbn__CostSp.GetValueOrDefault();
		}
		set
		{
			__pbn__CostSp = value;
		}
	}

	[ProtoMember(4, Name = "need_sp")]
	public float NeedSp
	{
		get
		{
			return __pbn__NeedSp.GetValueOrDefault();
		}
		set
		{
			__pbn__NeedSp = value;
		}
	}

	[ProtoMember(5, Name = "cd_timer")]
	public float CdTimer
	{
		get
		{
			return __pbn__CdTimer.GetValueOrDefault();
		}
		set
		{
			__pbn__CdTimer = value;
		}
	}

	[ProtoMember(6, Name = "max_charges_count")]
	public uint MaxChargesCount
	{
		get
		{
			return __pbn__MaxChargesCount.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxChargesCount = value;
		}
	}

	[ProtoMember(7, Name = "charges_counter")]
	public uint ChargesCounter
	{
		get
		{
			return __pbn__ChargesCounter.GetValueOrDefault();
		}
		set
		{
			__pbn__ChargesCounter = value;
		}
	}

	[ProtoMember(8, Name = "can_hold")]
	public bool CanHold
	{
		get
		{
			return __pbn__CanHold.GetValueOrDefault();
		}
		set
		{
			__pbn__CanHold = value;
		}
	}

	[ProtoMember(9, Name = "instant_trigger_anim_event_id")]
	[DefaultValue("")]
	public string InstantTriggerAnimEventId
	{
		get
		{
			return __pbn__InstantTriggerAnimEventId ?? "";
		}
		set
		{
			__pbn__InstantTriggerAnimEventId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillName()
	{
		return __pbn__SkillName != null;
	}

	public void ResetSkillName()
	{
		__pbn__SkillName = null;
	}

	public bool ShouldSerializeCd()
	{
		return __pbn__Cd.HasValue;
	}

	public void ResetCd()
	{
		__pbn__Cd = null;
	}

	public bool ShouldSerializeCostSp()
	{
		return __pbn__CostSp.HasValue;
	}

	public void ResetCostSp()
	{
		__pbn__CostSp = null;
	}

	public bool ShouldSerializeNeedSp()
	{
		return __pbn__NeedSp.HasValue;
	}

	public void ResetNeedSp()
	{
		__pbn__NeedSp = null;
	}

	public bool ShouldSerializeCdTimer()
	{
		return __pbn__CdTimer.HasValue;
	}

	public void ResetCdTimer()
	{
		__pbn__CdTimer = null;
	}

	public bool ShouldSerializeMaxChargesCount()
	{
		return __pbn__MaxChargesCount.HasValue;
	}

	public void ResetMaxChargesCount()
	{
		__pbn__MaxChargesCount = null;
	}

	public bool ShouldSerializeChargesCounter()
	{
		return __pbn__ChargesCounter.HasValue;
	}

	public void ResetChargesCounter()
	{
		__pbn__ChargesCounter = null;
	}

	public bool ShouldSerializeCanHold()
	{
		return __pbn__CanHold.HasValue;
	}

	public void ResetCanHold()
	{
		__pbn__CanHold = null;
	}

	public bool ShouldSerializeInstantTriggerAnimEventId()
	{
		return __pbn__InstantTriggerAnimEventId != null;
	}

	public void ResetInstantTriggerAnimEventId()
	{
		__pbn__InstantTriggerAnimEventId = null;
	}
}
