using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldGetActivityScheduleRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2428
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

	private uint? __pbn__SceneId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "scene_id")]
	public uint SceneId
	{
		get
		{
			return __pbn__SceneId.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneId = value;
		}
	}

	[ProtoMember(3, Name = "schedule_list")]
	public List<ChatworldActivitySchedule> ScheduleLists { get; } = new List<ChatworldActivitySchedule>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSceneId()
	{
		return __pbn__SceneId.HasValue;
	}

	public void ResetSceneId()
	{
		__pbn__SceneId = null;
	}
}
