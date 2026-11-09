using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessPlayerUpdateNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3445
	}

	private IExtension __pbn__extensionData;

	private EndlessType? __pbn__Type;

	[ProtoMember(1, Name = "player_data")]
	public OpenworldEndlessPlayer PlayerData { get; set; }

	[ProtoMember(2, Name = "brief_data")]
	public PlayerFriendBriefData BriefData { get; set; }

	[ProtoMember(3, Name = "type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
