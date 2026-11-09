using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetRecommendMasterPupilRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 656
	}

	private IExtension __pbn__extensionData;

	private MasterPupilRetcode.Retcode? __pbn__Retcode;

	private MasterPupilType? __pbn__Type;

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

	[ProtoMember(3, Name = "msg_list")]
	public List<MasterPupilApplyMsg> MsgLists { get; } = new List<MasterPupilApplyMsg>();


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
}
