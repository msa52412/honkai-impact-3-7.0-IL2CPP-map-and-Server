using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixFloorMapInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FloorId;

	private uint? __pbn__Width;

	private uint? __pbn__Height;

	private uint? __pbn__MapId;

	private uint? __pbn__StepNum;

	private uint? __pbn__DicePoint;

	[ProtoMember(1, Name = "floor_id")]
	public uint FloorId
	{
		get
		{
			return __pbn__FloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorId = value;
		}
	}

	[ProtoMember(2, Name = "grid_list")]
	public List<MatrixGrid> GridLists { get; } = new List<MatrixGrid>();


	[ProtoMember(3, Name = "width")]
	public uint Width
	{
		get
		{
			return __pbn__Width.GetValueOrDefault();
		}
		set
		{
			__pbn__Width = value;
		}
	}

	[ProtoMember(4, Name = "height")]
	public uint Height
	{
		get
		{
			return __pbn__Height.GetValueOrDefault();
		}
		set
		{
			__pbn__Height = value;
		}
	}

	[ProtoMember(5, Name = "map_id")]
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

	[ProtoMember(6, Name = "current_pos")]
	public MatrixGridPos CurrentPos { get; set; }

	[ProtoMember(7, Name = "event_grid_list")]
	public List<MatrixGrid> EventGridLists { get; } = new List<MatrixGrid>();


	[ProtoMember(8, Name = "step_num")]
	public uint StepNum
	{
		get
		{
			return __pbn__StepNum.GetValueOrDefault();
		}
		set
		{
			__pbn__StepNum = value;
		}
	}

	[ProtoMember(9, Name = "dice_point")]
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

	[ProtoMember(10, Name = "lottery_list")]
	public List<MatrixLottery> LotteryLists { get; } = new List<MatrixLottery>();


	[ProtoMember(11, Name = "buff_list")]
	public List<MatrixBuff> BuffLists { get; } = new List<MatrixBuff>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}

	public bool ShouldSerializeWidth()
	{
		return __pbn__Width.HasValue;
	}

	public void ResetWidth()
	{
		__pbn__Width = null;
	}

	public bool ShouldSerializeHeight()
	{
		return __pbn__Height.HasValue;
	}

	public void ResetHeight()
	{
		__pbn__Height = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeStepNum()
	{
		return __pbn__StepNum.HasValue;
	}

	public void ResetStepNum()
	{
		__pbn__StepNum = null;
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
