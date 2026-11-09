using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MiniMonopolyThrowDiceReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5983
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SectorId;

	private uint? __pbn__MapId;

	private uint? __pbn__ActivityId;

	[ProtoMember(1, Name = "sector_id")]
	public uint SectorId
	{
		get
		{
			return __pbn__SectorId.GetValueOrDefault();
		}
		set
		{
			__pbn__SectorId = value;
		}
	}

	[ProtoMember(2, Name = "map_id")]
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

	[ProtoMember(3, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSectorId()
	{
		return __pbn__SectorId.HasValue;
	}

	public void ResetSectorId()
	{
		__pbn__SectorId = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}
}
