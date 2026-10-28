using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DLCAvatarReviveReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3569
	}

	private IExtension __pbn__extensionData;

	private DLCAvatarReviveCostType? __pbn__CostType;

	private uint? __pbn__MapId;

	private uint? __pbn__AvatarId;

	[ProtoMember(1, Name = "cost_type")]
	[DefaultValue(DLCAvatarReviveCostType.DlcAvatarReviveCostTypeNone)]
	public DLCAvatarReviveCostType CostType
	{
		get
		{
			return __pbn__CostType.GetValueOrDefault();
		}
		set
		{
			__pbn__CostType = value;
		}
	}

	[ProtoMember(3, Name = "map_id")]
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

	[ProtoMember(4, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(5, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCostType()
	{
		return __pbn__CostType.HasValue;
	}

	public void ResetCostType()
	{
		__pbn__CostType = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}
}
