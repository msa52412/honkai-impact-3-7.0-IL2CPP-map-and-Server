using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DreamOpenDreamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5753
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_EXIST")]
		ActivityNotExist,
		[ProtoEnum(Name = "DREAM_NOT_EXIST")]
		DreamNotExist,
		[ProtoEnum(Name = "REWARD_NOT_EXIST")]
		RewardNotExist,
		[ProtoEnum(Name = "HAS_OPEN_DREAM")]
		HasOpenDream,
		[ProtoEnum(Name = "LOCKED")]
		Locked,
		[ProtoEnum(Name = "REWARD_TAKEN")]
		RewardTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UnlockId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "unlock_id")]
	public uint UnlockId
	{
		get
		{
			return __pbn__UnlockId.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUnlockId()
	{
		return __pbn__UnlockId.HasValue;
	}

	public void ResetUnlockId()
	{
		__pbn__UnlockId = null;
	}
}
