using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetScratchTicketRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4151
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__PlateId;

	private uint? __pbn__ResetTimes;

	private uint? __pbn__Round;

	private uint? __pbn__CorePlateEndRound;

	private bool? __pbn__HasGotCorePlate;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "plate_id")]
	public uint PlateId
	{
		get
		{
			return __pbn__PlateId.GetValueOrDefault();
		}
		set
		{
			__pbn__PlateId = value;
		}
	}

	[ProtoMember(4, Name = "point_list")]
	public List<ScratchTicketPoint> PointLists { get; } = new List<ScratchTicketPoint>();


	[ProtoMember(5, Name = "reset_times")]
	public uint ResetTimes
	{
		get
		{
			return __pbn__ResetTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetTimes = value;
		}
	}

	[ProtoMember(6, Name = "round")]
	public uint Round
	{
		get
		{
			return __pbn__Round.GetValueOrDefault();
		}
		set
		{
			__pbn__Round = value;
		}
	}

	[ProtoMember(7, Name = "core_plate_end_round")]
	public uint CorePlateEndRound
	{
		get
		{
			return __pbn__CorePlateEndRound.GetValueOrDefault();
		}
		set
		{
			__pbn__CorePlateEndRound = value;
		}
	}

	[ProtoMember(8, Name = "has_got_core_plate")]
	public bool HasGotCorePlate
	{
		get
		{
			return __pbn__HasGotCorePlate.GetValueOrDefault();
		}
		set
		{
			__pbn__HasGotCorePlate = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializePlateId()
	{
		return __pbn__PlateId.HasValue;
	}

	public void ResetPlateId()
	{
		__pbn__PlateId = null;
	}

	public bool ShouldSerializeResetTimes()
	{
		return __pbn__ResetTimes.HasValue;
	}

	public void ResetResetTimes()
	{
		__pbn__ResetTimes = null;
	}

	public bool ShouldSerializeRound()
	{
		return __pbn__Round.HasValue;
	}

	public void ResetRound()
	{
		__pbn__Round = null;
	}

	public bool ShouldSerializeCorePlateEndRound()
	{
		return __pbn__CorePlateEndRound.HasValue;
	}

	public void ResetCorePlateEndRound()
	{
		__pbn__CorePlateEndRound = null;
	}

	public bool ShouldSerializeHasGotCorePlate()
	{
		return __pbn__HasGotCorePlate.HasValue;
	}

	public void ResetHasGotCorePlate()
	{
		__pbn__HasGotCorePlate = null;
	}
}
