using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BingoCard : IExtensible
{
	[ProtoContract]
	public enum BingoCardStatus
	{
		[ProtoEnum(Name = "CARD_STATUS_WRONG")]
		CardStatusWrong,
		[ProtoEnum(Name = "CARD_STATUS_CORRECT")]
		CardStatusCorrect
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CardId;

	private BingoCardStatus? __pbn__CardStatus;

	[ProtoMember(1, Name = "card_id")]
	public uint CardId
	{
		get
		{
			return __pbn__CardId.GetValueOrDefault();
		}
		set
		{
			__pbn__CardId = value;
		}
	}

	[ProtoMember(2, Name = "card_status")]
	[DefaultValue(BingoCardStatus.CardStatusWrong)]
	public BingoCardStatus CardStatus
	{
		get
		{
			return __pbn__CardStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__CardStatus = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCardId()
	{
		return __pbn__CardId.HasValue;
	}

	public void ResetCardId()
	{
		__pbn__CardId = null;
	}

	public bool ShouldSerializeCardStatus()
	{
		return __pbn__CardStatus.HasValue;
	}

	public void ResetCardStatus()
	{
		__pbn__CardStatus = null;
	}
}
