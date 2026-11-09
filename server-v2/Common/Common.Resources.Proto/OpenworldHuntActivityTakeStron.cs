using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldHuntActivityTakeStrongholdRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4610
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__StrongholdId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "stronghold_id")]
	public uint StrongholdId
	{
		get
		{
			return __pbn__StrongholdId.GetValueOrDefault();
		}
		set
		{
			__pbn__StrongholdId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeStrongholdId()
	{
		return __pbn__StrongholdId.HasValue;
	}

	public void ResetStrongholdId()
	{
		__pbn__StrongholdId = null;
	}
}
