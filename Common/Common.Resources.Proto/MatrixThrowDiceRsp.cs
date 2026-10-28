using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixThrowDiceRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2346
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_DICE")]
		NoDice,
		[ProtoEnum(Name = "IN_FLOOR_EVENT")]
		InFloorEvent,
		[ProtoEnum(Name = "WAIT_CHOOSE")]
		WaitChoose,
		[ProtoEnum(Name = "HAVE_EVENT")]
		HaveEvent,
		[ProtoEnum(Name = "LACK_DICE")]
		LackDice
	}

	private IExtension __pbn__extensionData;

	private DiceType? __pbn__DiceType;

	private uint? __pbn__DicePoint;

	private uint? __pbn__BonusDicePoint;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "dice_type")]
	[DefaultValue(DiceType.DiceTypeNone)]
	public DiceType DiceType
	{
		get
		{
			return __pbn__DiceType.GetValueOrDefault();
		}
		set
		{
			__pbn__DiceType = value;
		}
	}

	[ProtoMember(3, Name = "dice_point")]
	public uint DicePoint
	{
		get
		{
			return __pbn__DicePoint.GetValueOrDefault();
		}
		set
		{
			__pbn__DicePoint = value;
		}
	}

	[ProtoMember(4, Name = "bonus_dice_point")]
	public uint BonusDicePoint
	{
		get
		{
			return __pbn__BonusDicePoint.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusDicePoint = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDiceType()
	{
		return __pbn__DiceType.HasValue;
	}

	public void ResetDiceType()
	{
		__pbn__DiceType = null;
	}

	public bool ShouldSerializeDicePoint()
	{
		return __pbn__DicePoint.HasValue;
	}

	public void ResetDicePoint()
	{
		__pbn__DicePoint = null;
	}

	public bool ShouldSerializeBonusDicePoint()
	{
		return __pbn__BonusDicePoint.HasValue;
	}

	public void ResetBonusDicePoint()
	{
		__pbn__BonusDicePoint = null;
	}
}
