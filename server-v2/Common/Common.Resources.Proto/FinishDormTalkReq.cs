using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishDormTalkReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 641
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PlotId;

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

	[ProtoMember(2, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

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
}
