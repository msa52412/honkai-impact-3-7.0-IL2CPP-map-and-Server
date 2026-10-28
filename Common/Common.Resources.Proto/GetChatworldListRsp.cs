using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetChatworldListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2412
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

	private uint? __pbn__ShowMaxId;

	private uint? __pbn__SceneId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatworld_list")]
	public List<ChatworldBrief> ChatworldLists { get; } = new List<ChatworldBrief>();


	[ProtoMember(3, Name = "show_max_id")]
	public uint ShowMaxId
	{
		get
		{
			return __pbn__ShowMaxId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowMaxId = value;
		}
	}

	[ProtoMember(4, Name = "scene_id")]
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

	[ProtoMember(5, Name = "redis_chatworld_list")]
	public uint[] RedisChatworldLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShowMaxId()
	{
		return __pbn__ShowMaxId.HasValue;
	}

	public void ResetShowMaxId()
	{
		__pbn__ShowMaxId = null;
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
