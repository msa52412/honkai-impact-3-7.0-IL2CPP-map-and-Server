using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBeastSyncReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2463
	}

	private IExtension __pbn__extensionData;

	private ulong? __pbn__BeastUniqueIdU64;

	private uint? __pbn__CostHp;

	[ProtoMember(1, Name = "beast_unique_id_u64")]
	public ulong BeastUniqueIdU64
	{
		get
		{
			return __pbn__BeastUniqueIdU64.GetValueOrDefault();
		}
		set
		{
			__pbn__BeastUniqueIdU64 = value;
		}
	}

	[ProtoMember(2, Name = "cost_hp")]
	public uint CostHp
	{
		get
		{
			return __pbn__CostHp.GetValueOrDefault();
		}
		set
		{
			__pbn__CostHp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeastUniqueIdU64()
	{
		return __pbn__BeastUniqueIdU64.HasValue;
	}

	public void ResetBeastUniqueIdU64()
	{
		__pbn__BeastUniqueIdU64 = null;
	}

	public bool ShouldSerializeCostHp()
	{
		return __pbn__CostHp.HasValue;
	}

	public void ResetCostHp()
	{
		__pbn__CostHp = null;
	}
}
