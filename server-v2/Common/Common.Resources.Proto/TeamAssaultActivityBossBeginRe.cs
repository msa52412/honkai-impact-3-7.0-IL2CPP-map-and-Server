using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TeamAssaultActivityBossBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4024
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BossIdx;

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

	[ProtoMember(2, Name = "boss_idx")]
	public uint BossIdx
	{
		get
		{
			return __pbn__BossIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__BossIdx = value;
		}
	}

	[ProtoMember(3, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

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

	public bool ShouldSerializeBossIdx()
	{
		return __pbn__BossIdx.HasValue;
	}

	public void ResetBossIdx()
	{
		__pbn__BossIdx = null;
	}
}
