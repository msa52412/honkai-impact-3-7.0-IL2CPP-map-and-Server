using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMasterPupilDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 569
	}

	private IExtension __pbn__extensionData;

	private MasterPupilRetcode.Retcode? __pbn__Retcode;

	private uint? __pbn__Type;

	private uint? __pbn__DismissedGraduatedPupilNum;

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
	public uint Type
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

	[ProtoMember(3, Name = "data_list")]
	public List<MasterPupilData> DataLists { get; } = new List<MasterPupilData>();


	[ProtoMember(4, Name = "dismissed_graduated_pupil_num")]
	public uint DismissedGraduatedPupilNum
	{
		get
		{
			return __pbn__DismissedGraduatedPupilNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DismissedGraduatedPupilNum = value;
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

	public bool ShouldSerializeDismissedGraduatedPupilNum()
	{
		return __pbn__DismissedGraduatedPupilNum.HasValue;
	}

	public void ResetDismissedGraduatedPupilNum()
	{
		__pbn__DismissedGraduatedPupilNum = null;
	}
}
