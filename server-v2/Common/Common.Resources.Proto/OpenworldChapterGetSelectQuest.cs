using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterGetSelectQuestReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4643
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
[ProtoContract]
public class OpenworldChapterGetSelectQuestRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4644
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "BUILDING_LOCKED")]
		BuildingLocked
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "quest_list")]
	public List<OpenworldQuest> QuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(3, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
