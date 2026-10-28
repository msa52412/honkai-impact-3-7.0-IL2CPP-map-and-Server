using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MiniMonopolyMap : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__CurrentGrid;

	private bool? __pbn__IsMapUnlocked;

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

	[ProtoMember(2, Name = "reward_taken_grid_list")]
	public uint[] RewardTakenGridLists { get; set; }

	[ProtoMember(3, Name = "current_grid")]
	public uint CurrentGrid
	{
		get
		{
			return __pbn__CurrentGrid.GetValueOrDefault();
		}
		set
		{
			__pbn__CurrentGrid = value;
		}
	}

	[ProtoMember(4, Name = "is_map_unlocked")]
	public bool IsMapUnlocked
	{
		get
		{
			return __pbn__IsMapUnlocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMapUnlocked = value;
		}
	}

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

	public bool ShouldSerializeCurrentGrid()
	{
		return __pbn__CurrentGrid.HasValue;
	}

	public void ResetCurrentGrid()
	{
		__pbn__CurrentGrid = null;
	}

	public bool ShouldSerializeIsMapUnlocked()
	{
		return __pbn__IsMapUnlocked.HasValue;
	}

	public void ResetIsMapUnlocked()
	{
		__pbn__IsMapUnlocked = null;
	}
}
