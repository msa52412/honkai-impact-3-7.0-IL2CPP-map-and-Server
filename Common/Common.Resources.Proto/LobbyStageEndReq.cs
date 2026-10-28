using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LobbyStageEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 313
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LobbyId;

	private uint? __pbn__ClientData;

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

	[ProtoMember(2, Name = "client_data")]
	public uint ClientData
	{
		get
		{
			return __pbn__ClientData.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientData = value;
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

	public bool ShouldSerializeClientData()
	{
		return __pbn__ClientData.HasValue;
	}

	public void ResetClientData()
	{
		__pbn__ClientData = null;
	}
}
