using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LobbyEnterFightErrorNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 321
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "CONNECT_TIMEOUT")]
		ConnectTimeout,
		[ProtoEnum(Name = "BEGIN_STAGE_FAIL")]
		BeginStageFail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LobbyId;

	private uint? __pbn__SubRetcode;

	[ProtoMember(1, Name = "lobby_id")]
	public uint LobbyId
	{
		get
		{
			return __pbn__LobbyId.GetValueOrDefault();
		}
		set
		{
			__pbn__LobbyId = value;
		}
	}

	[ProtoMember(2, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(3, Name = "uid_list")]
	public uint[] UidLists { get; set; }

	[ProtoMember(4, Name = "sub_retcode")]
	public uint SubRetcode
	{
		get
		{
			return __pbn__SubRetcode.GetValueOrDefault();
		}
		set
		{
			__pbn__SubRetcode = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeSubRetcode()
	{
		return __pbn__SubRetcode.HasValue;
	}

	public void ResetSubRetcode()
	{
		__pbn__SubRetcode = null;
	}
}
