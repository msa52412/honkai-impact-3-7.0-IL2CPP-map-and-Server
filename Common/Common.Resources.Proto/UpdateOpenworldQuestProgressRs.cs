using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateOpenworldQuestProgressRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4545
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FINISH_WAY_ERROR")]
		FinishWayError
	}

	private IExtension __pbn__extensionData;

	private OpenworldQuestFinishWay? __pbn__FinishWay;

	private string __pbn__FinishParaStr;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "finish_way")]
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
}
