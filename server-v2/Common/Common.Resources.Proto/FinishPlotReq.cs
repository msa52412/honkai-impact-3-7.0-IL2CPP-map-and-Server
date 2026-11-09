using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishPlotReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1378
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PlotType;

	private uint? __pbn__PlotId;

	private uint? __pbn__DialogId;

	private uint? __pbn__VisualNovelId;

	[ProtoMember(1, Name = "plot_type")]
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

	[ProtoMember(3, Name = "plot_id")]
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

	[ProtoMember(4, Name = "dialog_id")]
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

	[ProtoMember(5, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(6, Name = "visual_novel_id")]
	public uint VisualNovelId
	{
		get
		{
			return __pbn__VisualNovelId.GetValueOrDefault();
		}
		set
		{
			__pbn__VisualNovelId = value;
		}
	}

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

	public bool ShouldSerializeVisualNovelId()
	{
		return __pbn__VisualNovelId.HasValue;
	}

	public void ResetVisualNovelId()
	{
		__pbn__VisualNovelId = null;
	}
}
