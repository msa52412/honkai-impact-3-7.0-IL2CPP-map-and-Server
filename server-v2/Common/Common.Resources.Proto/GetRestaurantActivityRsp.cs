using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetRestaurantActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5861
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__FacilityLevel;

	private uint? __pbn__WeatherId;

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

	[ProtoMember(3, Name = "facility_level")]
	public uint FacilityLevel
	{
		get
		{
			return __pbn__FacilityLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__FacilityLevel = value;
		}
	}

	[ProtoMember(4, Name = "room_list")]
	public List<RestaurantRoom> RoomLists { get; } = new List<RestaurantRoom>();


	[ProtoMember(5, Name = "order_list")]
	public List<RestaurantOrder> OrderLists { get; } = new List<RestaurantOrder>();


	[ProtoMember(6, Name = "weather_id")]
	public uint WeatherId
	{
		get
		{
			return __pbn__WeatherId.GetValueOrDefault();
		}
		set
		{
			__pbn__WeatherId = value;
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

	public bool ShouldSerializeFacilityLevel()
	{
		return __pbn__FacilityLevel.HasValue;
	}

	public void ResetFacilityLevel()
	{
		__pbn__FacilityLevel = null;
	}

	public bool ShouldSerializeWeatherId()
	{
		return __pbn__WeatherId.HasValue;
	}

	public void ResetWeatherId()
	{
		__pbn__WeatherId = null;
	}
}
