using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarBonus : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BonusId;

	private uint? __pbn__CountDown;

	[ProtoMember(1, Name = "bonus_id")]
	public uint BonusId
	{
		get
		{
			return __pbn__BonusId.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusId = value;
		}
	}

	[ProtoMember(2, Name = "count_down")]
	public uint CountDown
	{
		get
		{
			return __pbn__CountDown.GetValueOrDefault();
		}
		set
		{
			__pbn__CountDown = value;
		}
	}

	[ProtoMember(3, Name = "buff")]
	public GodWarBuff Buff { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBonusId()
	{
		return __pbn__BonusId.HasValue;
	}

	public void ResetBonusId()
	{
		__pbn__BonusId = null;
	}

	public bool ShouldSerializeCountDown()
	{
		return __pbn__CountDown.HasValue;
	}

	public void ResetCountDown()
	{
		__pbn__CountDown = null;
	}
}
