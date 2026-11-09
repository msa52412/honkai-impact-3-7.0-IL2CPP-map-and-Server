using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VerifyGoogleOrderNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 247
	}

	[ProtoContract]
	public enum GoogleType
	{
		[ProtoEnum(Name = "GOOGLE_JAPAN")]
		GoogleJapan = 1,
		[ProtoEnum(Name = "GOOGLE_TW")]
		GoogleTw,
		[ProtoEnum(Name = "GOOGLE_KR")]
		GoogleKr,
		[ProtoEnum(Name = "GOOGLE_OVERSEAS")]
		GoogleOverseas,
		[ProtoEnum(Name = "GOOGLE_GLOBAL")]
		GoogleGlobal
	}

	private IExtension __pbn__extensionData;

	private GoogleType? __pbn__Channel;

	private string __pbn__Param;

	[ProtoMember(1, Name = "channel")]
	[DefaultValue(GoogleType.GoogleJapan)]
	public GoogleType Channel
	{
		get
		{
			return __pbn__Channel ?? GoogleType.GoogleJapan;
		}
		set
		{
			__pbn__Channel = value;
		}
	}

	[ProtoMember(2, Name = "param")]
	[DefaultValue("")]
	public string Param
	{
		get
		{
			return __pbn__Param ?? "";
		}
		set
		{
			__pbn__Param = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChannel()
	{
		return __pbn__Channel.HasValue;
	}

	public void ResetChannel()
	{
		__pbn__Channel = null;
	}

	public bool ShouldSerializeParam()
	{
		return __pbn__Param != null;
	}

	public void ResetParam()
	{
		__pbn__Param = null;
	}
}
