using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LobbyTeamPlayerNum : IExtensible
{
	private IExtension __pbn__extensionData;

	private LobbyStatus? __pbn__Status;

	private uint? __pbn__PlayerNum;

	[ProtoMember(1, Name = "status")]
	[DefaultValue(LobbyStatus.LobbyNone)]
	public LobbyStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(2, Name = "player_num")]
	public uint PlayerNum
	{
		get
		{
			return __pbn__PlayerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerNum = value;
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

	public bool ShouldSerializePlayerNum()
	{
		return __pbn__PlayerNum.HasValue;
	}

	public void ResetPlayerNum()
	{
		__pbn__PlayerNum = null;
	}
}
