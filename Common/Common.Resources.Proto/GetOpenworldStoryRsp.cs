using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOpenworldStoryRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4501
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

	private bool? __pbn__IsAll;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "finish_story_id_list")]
	public uint[] FinishStoryIdLists { get; set; }

	[ProtoMember(3, Name = "cur_story_list")]
	public List<OpenworldStory> CurStoryLists { get; } = new List<OpenworldStory>();


	[ProtoMember(4, Name = "finish_story_id_add_list")]
	public uint[] FinishStoryIdAddLists { get; set; }

	[ProtoMember(5, Name = "finish_story_id_del_list")]
	public uint[] FinishStoryIdDelLists { get; set; }

	[ProtoMember(6, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}
}
