using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GratuityStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageUniqueId;

	private uint? __pbn__ExpireTime;

	private uint? __pbn__AcceptUid;

	private uint? __pbn__StageId;

	private uint? __pbn__StageGroup;

	private uint? __pbn__PublishTime;

	[ProtoMember(1, Name = "stage_unique_id")]
	public uint StageUniqueId
	{
		get
		{
			return __pbn__StageUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageUniqueId = value;
		}
	}

	[ProtoMember(2, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	[ProtoMember(3, Name = "accept_uid")]
	public uint AcceptUid
	{
		get
		{
			return __pbn__AcceptUid.GetValueOrDefault();
		}
		set
		{
			__pbn__AcceptUid = value;
		}
	}

	[ProtoMember(4, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(5, Name = "stage_group")]
	public uint StageGroup
	{
		get
		{
			return __pbn__StageGroup.GetValueOrDefault();
		}
		set
		{
			__pbn__StageGroup = value;
		}
	}

	[ProtoMember(6, Name = "monster_list")]
	public List<GratuityMonster> MonsterLists { get; } = new List<GratuityMonster>();


	[ProtoMember(7, Name = "player_list")]
	public List<GratuityPlayer> PlayerLists { get; } = new List<GratuityPlayer>();


	[ProtoMember(8, Name = "publish_time")]
	public uint PublishTime
	{
		get
		{
			return __pbn__PublishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PublishTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageUniqueId()
	{
		return __pbn__StageUniqueId.HasValue;
	}

	public void ResetStageUniqueId()
	{
		__pbn__StageUniqueId = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}

	public bool ShouldSerializeAcceptUid()
	{
		return __pbn__AcceptUid.HasValue;
	}

	public void ResetAcceptUid()
	{
		__pbn__AcceptUid = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeStageGroup()
	{
		return __pbn__StageGroup.HasValue;
	}

	public void ResetStageGroup()
	{
		__pbn__StageGroup = null;
	}

	public bool ShouldSerializePublishTime()
	{
		return __pbn__PublishTime.HasValue;
	}

	public void ResetPublishTime()
	{
		__pbn__PublishTime = null;
	}
}
