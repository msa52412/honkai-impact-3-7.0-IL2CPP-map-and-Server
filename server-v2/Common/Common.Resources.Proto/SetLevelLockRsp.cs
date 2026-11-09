using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetLevelLockRsp : IExtensible
{
	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_MEET_MAX_LEVEL")]
		NotMeetMaxLevel,
		[ProtoEnum(Name = "NOT_NEXT_ID")]
		NotNextId
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1489
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LevelLockId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "level_lock_id")]
	public uint LevelLockId
	{
		get
		{
			return __pbn__LevelLockId.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelLockId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevelLockId()
	{
		return __pbn__LevelLockId.HasValue;
	}

	public void ResetLevelLockId()
	{
		__pbn__LevelLockId = null;
	}
}
