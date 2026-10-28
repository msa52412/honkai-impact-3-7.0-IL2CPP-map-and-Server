using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateOpenworldQuestProgressReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4544
	}

	private IExtension __pbn__extensionData;

	private OpenworldQuestFinishWay? __pbn__FinishWay;

	private string __pbn__FinishParaStr;

	private uint? __pbn__ProgressAdd;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "finish_way")]
	[DefaultValue(OpenworldQuestFinishWay.OpenworldQuestFinishWayNone)]
	public OpenworldQuestFinishWay FinishWay
	{
		get
		{
			return __pbn__FinishWay.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishWay = value;
		}
	}

	[ProtoMember(2, Name = "finish_para_str")]
	[DefaultValue("")]
	public string FinishParaStr
	{
		get
		{
			return __pbn__FinishParaStr ?? "";
		}
		set
		{
			__pbn__FinishParaStr = value;
		}
	}

	[ProtoMember(3, Name = "progress_add")]
	public uint ProgressAdd
	{
		get
		{
			return __pbn__ProgressAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgressAdd = value;
		}
	}

	[ProtoMember(4, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFinishWay()
	{
		return __pbn__FinishWay.HasValue;
	}

	public void ResetFinishWay()
	{
		__pbn__FinishWay = null;
	}

	public bool ShouldSerializeFinishParaStr()
	{
		return __pbn__FinishParaStr != null;
	}

	public void ResetFinishParaStr()
	{
		__pbn__FinishParaStr = null;
	}

	public bool ShouldSerializeProgressAdd()
	{
		return __pbn__ProgressAdd.HasValue;
	}

	public void ResetProgressAdd()
	{
		__pbn__ProgressAdd = null;
	}

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
