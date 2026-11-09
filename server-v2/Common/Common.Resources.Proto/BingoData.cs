using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BingoData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BingoId;

	private uint? __pbn__ResetTimes;

	[ProtoMember(1, Name = "bingo_id")]
	public uint BingoId
	{
		get
		{
			return __pbn__BingoId.GetValueOrDefault();
		}
		set
		{
			__pbn__BingoId = value;
		}
	}

	[ProtoMember(2, Name = "card_list")]
	public List<BingoCard> CardLists { get; } = new List<BingoCard>();


	[ProtoMember(3, Name = "reset_times")]
	public uint ResetTimes
	{
		get
		{
			return __pbn__ResetTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBingoId()
	{
		return __pbn__BingoId.HasValue;
	}

	public void ResetBingoId()
	{
		__pbn__BingoId = null;
	}

	public bool ShouldSerializeResetTimes()
	{
		return __pbn__ResetTimes.HasValue;
	}

	public void ResetResetTimes()
	{
		__pbn__ResetTimes = null;
	}
}
