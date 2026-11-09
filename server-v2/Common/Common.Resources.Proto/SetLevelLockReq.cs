using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetLevelLockReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1488
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LevelLockId;

	[ProtoMember(1, Name = "level_lock_id")]
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
