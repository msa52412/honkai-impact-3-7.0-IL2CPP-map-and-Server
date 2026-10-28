using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishPlotRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1379
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "PLOT_ERROR")]
		PlotError,
		[ProtoEnum(Name = "PLOT_FINISHED")]
		PlotFinished
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PlotType;

	private uint? __pbn__PlotId;

	private uint? __pbn__DialogId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "plot_type")]
	public uint PlotType
	{
		get
		{
			return __pbn__PlotType.GetValueOrDefault();
		}
		set
		{
			__pbn__PlotType = value;
		}
	}

	[ProtoMember(4, Name = "plot_id")]
	public uint PlotId
	{
		get
		{
			return __pbn__PlotId.GetValueOrDefault();
		}
		set
		{
			__pbn__PlotId = value;
		}
	}

	[ProtoMember(5, Name = "dialog_id")]
	public uint DialogId
	{
		get
		{
			return __pbn__DialogId.GetValueOrDefault();
		}
		set
		{
			__pbn__DialogId = value;
		}
	}

	[ProtoMember(6, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(7, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePlotType()
	{
		return __pbn__PlotType.HasValue;
	}

	public void ResetPlotType()
	{
		__pbn__PlotType = null;
	}

	public bool ShouldSerializePlotId()
	{
		return __pbn__PlotId.HasValue;
	}

	public void ResetPlotId()
	{
		__pbn__PlotId = null;
	}

	public bool ShouldSerializeDialogId()
	{
		return __pbn__DialogId.HasValue;
	}

	public void ResetDialogId()
	{
		__pbn__DialogId = null;
	}
}
