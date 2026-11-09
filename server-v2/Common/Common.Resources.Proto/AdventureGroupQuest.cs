using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AdventureGroupQuest : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	private uint? __pbn__QuestId;

	private uint? __pbn__AcceptTime;

	private uint? __pbn__Status;

	private uint? __pbn__ElfId;

	[ProtoMember(1, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	[ProtoMember(2, Name = "quest_id")]
	public uint QuestId
	{
		get
		{
			return __pbn__QuestId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestId = value;
		}
	}

	[ProtoMember(3, Name = "accept_time")]
	public uint AcceptTime
	{
		get
		{
			return __pbn__AcceptTime.GetValueOrDefault();
		}
		set
		{
			__pbn__AcceptTime = value;
		}
	}

	[ProtoMember(4, Name = "avatar_list")]
	public uint[] AvatarLists { get; set; }

	[ProtoMember(5, Name = "status")]
	public uint Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(6, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}

	public bool ShouldSerializeQuestId()
	{
		return __pbn__QuestId.HasValue;
	}

	public void ResetQuestId()
	{
		__pbn__QuestId = null;
	}

	public bool ShouldSerializeAcceptTime()
	{
		return __pbn__AcceptTime.HasValue;
	}

	public void ResetAcceptTime()
	{
		__pbn__AcceptTime = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}
}
