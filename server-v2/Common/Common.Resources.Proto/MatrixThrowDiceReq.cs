using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixThrowDiceReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2345
	}

	private IExtension __pbn__extensionData;

	private DiceType? __pbn__DiceType;

	private uint? __pbn__DicePoint;

	[ProtoMember(1, Name = "dice_type")]
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

	[ProtoMember(2, Name = "dice_point")]
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
}
