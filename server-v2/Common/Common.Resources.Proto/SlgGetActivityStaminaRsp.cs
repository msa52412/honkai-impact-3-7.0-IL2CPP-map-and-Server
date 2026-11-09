using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgGetActivityStaminaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6058
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityStaminaNum;

	private uint? __pbn__LastRecoverActivityStaminaTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_stamina_num")]
	public uint ActivityStaminaNum
	{
		get
		{
			return __pbn__ActivityStaminaNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityStaminaNum = value;
		}
	}

	[ProtoMember(3, Name = "last_recover_activity_stamina_time")]
	public uint LastRecoverActivityStaminaTime
	{
		get
		{
			return __pbn__LastRecoverActivityStaminaTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRecoverActivityStaminaTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityStaminaNum()
	{
		return __pbn__ActivityStaminaNum.HasValue;
	}

	public void ResetActivityStaminaNum()
	{
		__pbn__ActivityStaminaNum = null;
	}

	public bool ShouldSerializeLastRecoverActivityStaminaTime()
	{
		return __pbn__LastRecoverActivityStaminaTime.HasValue;
	}

	public void ResetLastRecoverActivityStaminaTime()
	{
		__pbn__LastRecoverActivityStaminaTime = null;
	}
}
