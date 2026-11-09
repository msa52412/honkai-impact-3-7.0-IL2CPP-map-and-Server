using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGardenScheduleRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4296
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GardenId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private bool? __pbn__IsGardenOpened;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "garden_id")]
	public uint GardenId
	{
		get
		{
			return __pbn__GardenId.GetValueOrDefault();
		}
		set
		{
			__pbn__GardenId = value;
		}
	}

	[ProtoMember(3, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(4, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(5, Name = "is_garden_opened")]
	public bool IsGardenOpened
	{
		get
		{
			return __pbn__IsGardenOpened.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGardenOpened = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGardenId()
	{
		return __pbn__GardenId.HasValue;
	}

	public void ResetGardenId()
	{
		__pbn__GardenId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeIsGardenOpened()
	{
		return __pbn__IsGardenOpened.HasValue;
	}

	public void ResetIsGardenOpened()
	{
		__pbn__IsGardenOpened = null;
	}
}
