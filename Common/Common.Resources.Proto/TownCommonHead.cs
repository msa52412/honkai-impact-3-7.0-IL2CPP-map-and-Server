using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownCommonHead : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TownId;

	private uint? __pbn__TowerId;

	private uint? __pbn__GameRound;

	[ProtoMember(1, Name = "town_id")]
	public uint TownId
	{
		get
		{
			return __pbn__TownId.GetValueOrDefault();
		}
		set
		{
			__pbn__TownId = value;
		}
	}

	[ProtoMember(2, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(3, Name = "game_round")]
	public uint GameRound
	{
		get
		{
			return __pbn__GameRound.GetValueOrDefault();
		}
		set
		{
			__pbn__GameRound = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTownId()
	{
		return __pbn__TownId.HasValue;
	}

	public void ResetTownId()
	{
		__pbn__TownId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeGameRound()
	{
		return __pbn__GameRound.HasValue;
	}

	public void ResetGameRound()
	{
		__pbn__GameRound = null;
	}
}
