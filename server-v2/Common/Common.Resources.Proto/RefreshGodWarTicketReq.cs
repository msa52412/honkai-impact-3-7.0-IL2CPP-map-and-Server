using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RefreshGodWarTicketReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6189
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	[ProtoMember(1, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}
}
