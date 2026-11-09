using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActivityBuff : IExtensible
{
	[ProtoContract]
	public class Buff : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__BuffId;

		private uint? __pbn__BuffNum;

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
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__ChargeValue;

	private uint? __pbn__BuffChargedTimes;

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

	[ProtoMember(2, Name = "candidate_buff_id_list")]
	public uint[] CandidateBuffIdLists { get; set; }

	[ProtoMember(3, Name = "buff_list")]
	public List<Buff> BuffLists { get; } = new List<Buff>();


	[ProtoMember(4, Name = "charge_value")]
	public uint ChargeValue
	{
		get
		{
			return __pbn__ChargeValue.GetValueOrDefault();
		}
		set
		{
			__pbn__ChargeValue = value;
		}
	}

	[ProtoMember(5, Name = "buff_charged_times")]
	public uint BuffChargedTimes
	{
		get
		{
			return __pbn__BuffChargedTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffChargedTimes = value;
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

	public bool ShouldSerializeChargeValue()
	{
		return __pbn__ChargeValue.HasValue;
	}

	public void ResetChargeValue()
	{
		__pbn__ChargeValue = null;
	}

	public bool ShouldSerializeBuffChargedTimes()
	{
		return __pbn__BuffChargedTimes.HasValue;
	}

	public void ResetBuffChargedTimes()
	{
		__pbn__BuffChargedTimes = null;
	}
}
