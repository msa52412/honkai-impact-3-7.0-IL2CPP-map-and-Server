using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarLineupInfoNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4811
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleUniqId;

	[ProtoMember(1, Name = "battle_uniq_id")]
	public uint BattleUniqId
	{
		get
		{
			return __pbn__BattleUniqId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleUniqId = value;
		}
	}

	[ProtoMember(2, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleUniqId()
	{
		return __pbn__BattleUniqId.HasValue;
	}

	public void ResetBattleUniqId()
	{
		__pbn__BattleUniqId = null;
	}
}
