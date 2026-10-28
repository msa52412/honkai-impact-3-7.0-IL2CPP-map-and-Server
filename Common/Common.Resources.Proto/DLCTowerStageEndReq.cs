using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DLCTowerStageEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3565
	}

	private IExtension __pbn__extensionData;

	private StageEndStatus? __pbn__EndStatus;

	private uint? __pbn__Progress;

	private uint? __pbn__StagePassTime;

	[ProtoMember(1, Name = "end_status")]
	[DefaultValue(StageEndStatus.StageWin)]
	public StageEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? StageEndStatus.StageWin;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(2, Name = "avatar_info_list")]
	public List<DLCTowerAvatarInfo> AvatarInfoLists { get; } = new List<DLCTowerAvatarInfo>();


	[ProtoMember(3, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(4, Name = "stage_pass_time")]
	public uint StagePassTime
	{
		get
		{
			return __pbn__StagePassTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StagePassTime = value;
		}
	}

	[ProtoMember(6, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeStagePassTime()
	{
		return __pbn__StagePassTime.HasValue;
	}

	public void ResetStagePassTime()
	{
		__pbn__StagePassTime = null;
	}
}
