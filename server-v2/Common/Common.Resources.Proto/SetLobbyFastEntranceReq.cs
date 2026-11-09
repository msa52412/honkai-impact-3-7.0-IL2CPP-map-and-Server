using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetLobbyFastEntranceReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 319
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LobbyId;

	private bool? __pbn__IsOpen;

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

	[ProtoMember(2, Name = "is_open")]
	public bool IsOpen
	{
		get
		{
			return __pbn__IsOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpen = value;
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

	public bool ShouldSerializeIsOpen()
	{
		return __pbn__IsOpen.HasValue;
	}

	public void ResetIsOpen()
	{
		__pbn__IsOpen = null;
	}
}
