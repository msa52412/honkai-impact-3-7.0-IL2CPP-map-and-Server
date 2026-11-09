using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishRpgTalePlotReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2305
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PlotId;

	private uint? __pbn__DialogId;

	private uint? __pbn__TaleId;

	[ProtoMember(1, Name = "plot_id")]
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

	[ProtoMember(2, Name = "dialog_id")]
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

	[ProtoMember(3, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
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

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}
}
