using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishSurveyNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4243
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SurveyId;

	[ProtoMember(1, Name = "survey_id")]
	public uint SurveyId
	{
		get
		{
			return __pbn__SurveyId.GetValueOrDefault();
		}
		set
		{
			__pbn__SurveyId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSurveyId()
	{
		return __pbn__SurveyId.HasValue;
	}

	public void ResetSurveyId()
	{
		__pbn__SurveyId = null;
	}
}
