using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayGetUploadListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5415
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TodayUploadNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "replay_list")]
	public List<Replay> ReplayLists { get; } = new List<Replay>();


	[ProtoMember(3, Name = "today_upload_num")]
	public uint TodayUploadNum
	{
		get
		{
			return __pbn__TodayUploadNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayUploadNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTodayUploadNum()
	{
		return __pbn__TodayUploadNum.HasValue;
	}

	public void ResetTodayUploadNum()
	{
		__pbn__TodayUploadNum = null;
	}
}
