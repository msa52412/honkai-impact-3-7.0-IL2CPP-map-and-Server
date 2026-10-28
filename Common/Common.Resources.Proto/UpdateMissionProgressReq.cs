using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateMissionProgressReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 117
	}

	private IExtension __pbn__extensionData;

	private MissionFinishWay? __pbn__FinishWay;

	private uint? __pbn__FinishPara;

	private string __pbn__FinishParaStr;

	private uint? __pbn__ProgressAdd;

	private uint? __pbn__StageId;

	[ProtoMember(1, Name = "finish_way")]
	[DefaultValue(MissionFinishWay.MissionFinishNone)]
	public MissionFinishWay FinishWay
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

	[ProtoMember(2, Name = "finish_para")]
	public uint FinishPara
	{
		get
		{
			return __pbn__FinishPara.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishPara = value;
		}
	}

	[ProtoMember(3, Name = "finish_para_str")]
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

	[ProtoMember(4, Name = "progress_add")]
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

	[ProtoMember(5, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
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

	public bool ShouldSerializeFinishPara()
	{
		return __pbn__FinishPara.HasValue;
	}

	public void ResetFinishPara()
	{
		__pbn__FinishPara = null;
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

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}
}
