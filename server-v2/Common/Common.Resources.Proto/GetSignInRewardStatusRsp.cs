using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSignInRewardStatusRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 122
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

	private bool? __pbn__IsNeedSignIn;

	private uint? __pbn__NextSignInDay;

	private uint? __pbn__NextSignInRewardId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "is_need_sign_in")]
	public bool IsNeedSignIn
	{
		get
		{
			return __pbn__IsNeedSignIn.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNeedSignIn = value;
		}
	}

	[ProtoMember(3, Name = "next_sign_in_day")]
	public uint NextSignInDay
	{
		get
		{
			return __pbn__NextSignInDay.GetValueOrDefault();
		}
		set
		{
			__pbn__NextSignInDay = value;
		}
	}

	[ProtoMember(4, Name = "next_sign_in_reward_id")]
	public uint NextSignInRewardId
	{
		get
		{
			return __pbn__NextSignInRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__NextSignInRewardId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsNeedSignIn()
	{
		return __pbn__IsNeedSignIn.HasValue;
	}

	public void ResetIsNeedSignIn()
	{
		__pbn__IsNeedSignIn = null;
	}

	public bool ShouldSerializeNextSignInDay()
	{
		return __pbn__NextSignInDay.HasValue;
	}

	public void ResetNextSignInDay()
	{
		__pbn__NextSignInDay = null;
	}

	public bool ShouldSerializeNextSignInRewardId()
	{
		return __pbn__NextSignInRewardId.HasValue;
	}

	public void ResetNextSignInRewardId()
	{
		__pbn__NextSignInRewardId = null;
	}
}
