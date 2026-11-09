using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AcceptGratuityStageReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4102
	}

	private IExtension __pbn__extensionData;

	private GratuityAcceptType? __pbn__AcceptType;

	[ProtoMember(1, Name = "accept_type")]
	[DefaultValue(GratuityAcceptType.GratuityAcceptNone)]
	public GratuityAcceptType AcceptType
	{
		get
		{
			return __pbn__AcceptType.GetValueOrDefault();
		}
		set
		{
			__pbn__AcceptType = value;
		}
	}

	[ProtoMember(2, Name = "player_info")]
	public GratuityPlayerInfo PlayerInfo { get; set; }

	[ProtoMember(3, Name = "stage_meta_list")]
	public List<GratuityStageMeta> StageMetaLists { get; } = new List<GratuityStageMeta>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAcceptType()
	{
		return __pbn__AcceptType.HasValue;
	}

	public void ResetAcceptType()
	{
		__pbn__AcceptType = null;
	}
}
