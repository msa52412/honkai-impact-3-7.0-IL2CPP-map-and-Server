using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DealMasterPupilRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 573
	}

	private IExtension __pbn__extensionData;

	private MasterPupilRetcode.Retcode? __pbn__Retcode;

	private uint? __pbn__Uid;

	private MasterPupilOpType? __pbn__Op;

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

	[ProtoMember(2, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(3, Name = "op")]
	[DefaultValue(MasterPupilOpType.MasterPupilOpNone)]
	public MasterPupilOpType Op
	{
		get
		{
			return __pbn__Op.GetValueOrDefault();
		}
		set
		{
			__pbn__Op = value;
		}
	}

	[ProtoMember(4, Name = "type")]
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

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeOp()
	{
		return __pbn__Op.HasValue;
	}

	public void ResetOp()
	{
		__pbn__Op = null;
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
