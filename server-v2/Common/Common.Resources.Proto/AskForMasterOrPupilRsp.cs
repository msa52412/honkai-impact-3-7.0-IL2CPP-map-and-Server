using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AskForMasterOrPupilRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 571
	}

	private IExtension __pbn__extensionData;

	private MasterPupilRetcode.Retcode? __pbn__Retcode;

	private MasterPupilType? __pbn__Type;

	private uint? __pbn__AskUid;

	[ProtoMember(1, Name = "retcode")]
	[DefaultValue(MasterPupilRetcode.Retcode.Succ)]
	public MasterPupilRetcode.Retcode Retcode
	{
		get
		{
			return __pbn__Retcode.GetValueOrDefault();
		}
		set
		{
			__pbn__Retcode = value;
		}
	}

	[ProtoMember(2, Name = "type")]
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

	[ProtoMember(3, Name = "ask_uid")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRetcode()
	{
		return __pbn__Retcode.HasValue;
	}

	public void ResetRetcode()
	{
		__pbn__Retcode = null;
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
}
