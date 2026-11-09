using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BattleChatParam : IExtensible
{
	private IExtension __pbn__extensionData;

	private BattleMode? __pbn__BattleMode;

	private uint? __pbn__BattlePara;

	[ProtoMember(1, Name = "battle_mode")]
	[DefaultValue(BattleMode.BattleModeNone)]
	public BattleMode BattleMode
	{
		get
		{
			return __pbn__BattleMode.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleMode = value;
		}
	}

	[ProtoMember(2, Name = "battle_para")]
	public uint BattlePara
	{
		get
		{
			return __pbn__BattlePara.GetValueOrDefault();
		}
		set
		{
			__pbn__BattlePara = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleMode()
	{
		return __pbn__BattleMode.HasValue;
	}

	public void ResetBattleMode()
	{
		__pbn__BattleMode = null;
	}

	public bool ShouldSerializeBattlePara()
	{
		return __pbn__BattlePara.HasValue;
	}

	public void ResetBattlePara()
	{
		__pbn__BattlePara = null;
	}
}
