using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomSwitchMemberStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3173
	}

	private IExtension __pbn__extensionData;

	private RoomMemberStatus? __pbn__Status;

	private RoomClientStatus? __pbn__ClientStatus;

	[ProtoMember(1, Name = "status")]
	[DefaultValue(RoomMemberStatus.RoomUnready)]
	public RoomMemberStatus Status
	{
		get
		{
			return __pbn__Status ?? RoomMemberStatus.RoomUnready;
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(2, Name = "client_status")]
	[DefaultValue(RoomClientStatus.RoomClientNone)]
	public RoomClientStatus ClientStatus
	{
		get
		{
			return __pbn__ClientStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientStatus = value;
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

	public bool ShouldSerializeClientStatus()
	{
		return __pbn__ClientStatus.HasValue;
	}

	public void ResetClientStatus()
	{
		__pbn__ClientStatus = null;
	}
}
