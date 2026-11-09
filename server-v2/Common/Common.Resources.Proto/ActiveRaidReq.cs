using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActiveRaidReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1624
	}

	private IExtension __pbn__extensionData;

	private RaidActiveType? __pbn__ActiveType;

	[ProtoMember(1, Name = "active_type")]
	[DefaultValue(RaidActiveType.RaidActiveNone)]
	public RaidActiveType ActiveType
	{
		get
		{
			return __pbn__ActiveType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActiveType()
	{
		return __pbn__ActiveType.HasValue;
	}

	public void ResetActiveType()
	{
		__pbn__ActiveType = null;
	}
}
