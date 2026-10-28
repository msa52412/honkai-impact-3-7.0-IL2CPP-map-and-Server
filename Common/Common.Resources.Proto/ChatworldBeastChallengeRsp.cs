using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBeastChallengeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2462
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "DAILY_LIMIT")]
		DailyLimit,
		[ProtoEnum(Name = "PLAYER_FULL")]
		PlayerFull,
		[ProtoEnum(Name = "EXPIRE")]
		Expire,
		[ProtoEnum(Name = "FINISH")]
		Finish,
		[ProtoEnum(Name = "TIME_NOT_ENOUGH")]
		TimeNotEnough
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__SceneId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "beast")]
	public ChatworldBeast Beast { get; set; }

	[ProtoMember(3, Name = "chatworld_id")]
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

	public bool ShouldSerializeSceneId()
	{
		return __pbn__SceneId.HasValue;
	}

	public void ResetSceneId()
	{
		__pbn__SceneId = null;
	}
}
