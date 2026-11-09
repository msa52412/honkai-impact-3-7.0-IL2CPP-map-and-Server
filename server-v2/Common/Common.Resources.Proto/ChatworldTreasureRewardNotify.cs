using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldTreasureRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2419
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__TreasureId;

	[ProtoMember(1, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(2, Name = "treasure_id")]
	public uint TreasureId
	{
		get
		{
			return __pbn__TreasureId.GetValueOrDefault();
		}
		set
		{
			__pbn__TreasureId = value;
		}
	}

	[ProtoMember(3, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeTreasureId()
	{
		return __pbn__TreasureId.HasValue;
	}

	public void ResetTreasureId()
	{
		__pbn__TreasureId = null;
	}
}
