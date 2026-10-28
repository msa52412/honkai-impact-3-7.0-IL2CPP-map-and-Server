using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessBattle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__SceneGear;

	[ProtoMember(1, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(2, Name = "trap_list")]
	public List<OpenworldEndlessTrap> TrapLists { get; } = new List<OpenworldEndlessTrap>();


	[ProtoMember(3, Name = "scene_gear")]
	public uint SceneGear
	{
		get
		{
			return __pbn__SceneGear.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneGear = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeSceneGear()
	{
		return __pbn__SceneGear.HasValue;
	}

	public void ResetSceneGear()
	{
		__pbn__SceneGear = null;
	}
}
