using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AsMasterPupilCardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 567
	}

	private IExtension __pbn__extensionData;

	private MasterPupilRetcode.Retcode? __pbn__Retcode;

	private uint? __pbn__CdEndTime;

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

	[ProtoMember(2, Name = "cd_end_time")]
	public uint CdEndTime
	{
		get
		{
			return __pbn__CdEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CdEndTime = value;
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

	public bool ShouldSerializeCdEndTime()
	{
		return __pbn__CdEndTime.HasValue;
	}

	public void ResetCdEndTime()
	{
		__pbn__CdEndTime = null;
	}
}
