using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AskForMasterOrPupilReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 570
	}

	private IExtension __pbn__extensionData;

	private MasterPupilType? __pbn__Type;

	private uint? __pbn__AskUid;

	private uint? __pbn__Time;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(MasterPupilType.MasterPupilNoneType)]
	public MasterPupilType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "ask_uid")]
	public uint AskUid
	{
		get
		{
			return __pbn__AskUid.GetValueOrDefault();
		}
		set
		{
			__pbn__AskUid = value;
		}
	}

	[ProtoMember(3, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeAskUid()
	{
		return __pbn__AskUid.HasValue;
	}

	public void ResetAskUid()
	{
		__pbn__AskUid = null;
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}
}
