using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWeeklyReportRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5101
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

	private uint? __pbn__TargetUid;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "ex_boss_data_list")]
	public List<WeeklyReportExBoss> ExBossDataLists { get; } = new List<WeeklyReportExBoss>();


	[ProtoMember(3, Name = "endless_data_list")]
	public List<WeeklyReportEndless> EndlessDataLists { get; } = new List<WeeklyReportEndless>();


	[ProtoMember(4, Name = "ultra_endless_list")]
	public List<WeeklyReportUltraEndless> UltraEndlessLists { get; } = new List<WeeklyReportUltraEndless>();


	[ProtoMember(5, Name = "block_zone")]
	public WeeklyReportBlockZone BlockZone { get; set; }

	[ProtoMember(6, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}
}
