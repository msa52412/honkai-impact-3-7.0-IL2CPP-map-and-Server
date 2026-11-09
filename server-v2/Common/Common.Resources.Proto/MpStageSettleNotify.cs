using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageSettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 391
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

	[ProtoMember(1, Name = "team_id")]
	public uint TeamId
	{
		get
		{
			return __pbn__TeamId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamId = value;
		}
	}

	[ProtoMember(2, Name = "settle_list")]
	public List<MpStageSettle> SettleLists { get; } = new List<MpStageSettle>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
	}
}
