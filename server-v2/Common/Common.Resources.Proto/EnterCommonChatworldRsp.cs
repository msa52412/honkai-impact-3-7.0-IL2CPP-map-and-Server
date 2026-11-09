using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EnterCommonChatworldRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2401
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CHATWORLD_ID_ERROR")]
		ChatworldIdError,
		[ProtoEnum(Name = "ENTER_ERROR")]
		EnterError,
		[ProtoEnum(Name = "CHATWORLD_FULL")]
		ChatworldFull,
		[ProtoEnum(Name = "LACK_LEVEL")]
		LackLevel,
		[ProtoEnum(Name = "NO_QAVATAR")]
		NoQavatar,
		[ProtoEnum(Name = "NOT_JOIN_ACTIVITY")]
		NotJoinActivity,
		[ProtoEnum(Name = "SCENE_NOT_EXIST")]
		SceneNotExist,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "IS_ENTERING")]
		IsEntering,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__Time;

	private uint? __pbn__SceneId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(3, Name = "player_info_list")]
	public List<ChatworldEntity> PlayerInfoLists { get; } = new List<ChatworldEntity>();


	[ProtoMember(4, Name = "self_info")]
	public ChatworldEntity SelfInfo { get; set; }

	[ProtoMember(5, Name = "npc_red_envelope_list")]
	public List<CommonChatworldRedEnvelope> NpcRedEnvelopeLists { get; } = new List<CommonChatworldRedEnvelope>();


	[ProtoMember(6, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	[ProtoMember(7, Name = "scene_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
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
