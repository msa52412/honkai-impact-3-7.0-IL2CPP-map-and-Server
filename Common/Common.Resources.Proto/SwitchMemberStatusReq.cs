using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchMemberStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 309
	}

	private IExtension __pbn__extensionData;

	private LobbyMemberStatus? __pbn__Status;

	[ProtoMember(1, Name = "status")]
	[DefaultValue(LobbyMemberStatus.LobbyMemberUnready)]
	public LobbyMemberStatus Status
	{
		get
		{
			return __pbn__Status ?? LobbyMemberStatus.LobbyMemberUnready;
		}
		set
		{
			__pbn__Status = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}
}
