using System.Linq;

namespace Common.Utils.ExcelReader;

public class PlayerLevelLock : BaseExcelReader<PlayerLevelLock, PlayerLevelLockExcel>
{
	public override string FileName => "PlayerLevelLock.json";

	/// <summary>按 LockID 取一档等级锁；取不到返回 null。</summary>
	public PlayerLevelLockExcel? Get(uint lockId)
	{
		return base.All.Where((PlayerLevelLockExcel x) => x.LockID == lockId).FirstOrDefault();
	}

	/// <summary>
	/// 取当前档位「下一档」。按 LockID 升序找第一个比它大的。
	/// 已经是最高档时返回 null（对应 SetLevelLockRsp.Retcode.NOT_NEXT_ID）。
	/// </summary>
	public PlayerLevelLockExcel? Next(uint lockId)
	{
		return base.All.Where((PlayerLevelLockExcel x) => x.LockID > lockId)
			.OrderBy((PlayerLevelLockExcel x) => x.LockID)
			.FirstOrDefault();
	}

	/// <summary>最低档（未突破那档），拿不到表时兜底用。</summary>
	public uint MinLockId()
	{
		if (base.All == null || base.All.Length == 0)
		{
			return 1u;
		}
		return base.All.Min((PlayerLevelLockExcel x) => x.LockID);
	}

	/// <summary>最高档。</summary>
	public uint MaxLockId()
	{
		if (base.All == null || base.All.Length == 0)
		{
			return 1u;
		}
		return base.All.Max((PlayerLevelLockExcel x) => x.LockID);
	}
}
