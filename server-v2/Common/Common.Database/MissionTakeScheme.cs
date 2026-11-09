namespace Common.Database;

/// <summary>
/// 一条「已领取的任务/成就奖励」。(OwnerUid, MissionId) 唯一。
///
/// 为什么要落库：任务列表每次 GetMissionDataReq 都是重算的，服务端没有「已领」记忆的话，
/// 重登后客户端又把按钮画成「领取」，点一次就再发一次奖。
/// </summary>
public class MissionTakeScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public uint MissionId { get; set; }
}
