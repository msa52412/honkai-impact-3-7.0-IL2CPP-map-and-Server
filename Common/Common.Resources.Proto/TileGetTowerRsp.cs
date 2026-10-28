using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileGetTowerRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4905
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurFloorId;

	private uint? __pbn__TowerId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_floor_id")]
	public uint CurFloorId
	{
		get
		{
			return __pbn__CurFloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFloorId = value;
		}
	}

	[ProtoMember(3, Name = "finished_dialog_id_list")]
	public uint[] FinishedDialogIdLists { get; set; }

	[ProtoMember(4, Name = "settled_event_id_list")]
	public uint[] SettledEventIdLists { get; set; }

	[ProtoMember(5, Name = "floor_list")]
	public List<TileFloor> FloorLists { get; } = new List<TileFloor>();


	[ProtoMember(6, Name = "tower_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurFloorId()
	{
		return __pbn__CurFloorId.HasValue;
	}

	public void ResetCurFloorId()
	{
		__pbn__CurFloorId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}
}
