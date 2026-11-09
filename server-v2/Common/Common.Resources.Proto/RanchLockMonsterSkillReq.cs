using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchLockMonsterSkillReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5849
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__UniqueId;

	[ProtoMember(1, Name = "activity_id")]
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

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "lock_skill_idx_list")]
	public uint[] LockSkillIdxLists { get; set; }

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

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}
}
