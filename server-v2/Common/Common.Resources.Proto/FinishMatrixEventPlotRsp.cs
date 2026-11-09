using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishMatrixEventPlotRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2337
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

	private uint? __pbn__PlotId;

	private uint? __pbn__DialogId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "plot_id")]
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

	[ProtoMember(3, Name = "dialog_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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
