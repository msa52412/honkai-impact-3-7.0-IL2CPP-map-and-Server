using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleRefreshGadgetForecastNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5622
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RefreshId;

	private ulong? __pbn__RefreshTimeMsU64;

	[ProtoMember(1, Name = "refresh_id")]
	public uint RefreshId
	{
		get
		{
			return __pbn__RefreshId.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshId = value;
		}
	}

	[ProtoMember(2, Name = "refresh_time_ms_u64")]
	public ulong RefreshTimeMsU64
	{
		get
		{
			return __pbn__RefreshTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshTimeMsU64 = value;
		}
	}

	[ProtoMember(3, Name = "spawn_point_id_list")]
	public uint[] SpawnPointIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRefreshId()
	{
		return __pbn__RefreshId.HasValue;
	}

	public void ResetRefreshId()
	{
		__pbn__RefreshId = null;
	}

	public bool ShouldSerializeRefreshTimeMsU64()
	{
		return __pbn__RefreshTimeMsU64.HasValue;
	}

	public void ResetRefreshTimeMsU64()
	{
		__pbn__RefreshTimeMsU64 = null;
	}
}
