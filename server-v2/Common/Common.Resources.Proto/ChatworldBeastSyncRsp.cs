using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBeastSyncRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2464
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

	private ulong? __pbn__BeastUniqueIdU64;

	private uint? __pbn__FromUid;

	private uint? __pbn__CostHp;

	private uint? __pbn__LeftHp;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "beast_unique_id_u64")]
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

	[ProtoMember(3, Name = "from_uid")]
	public uint FromUid
	{
		get
		{
			return __pbn__FromUid.GetValueOrDefault();
		}
		set
		{
			__pbn__FromUid = value;
		}
	}

	[ProtoMember(4, Name = "cost_hp")]
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

	[ProtoMember(5, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
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

	public bool ShouldSerializeFromUid()
	{
		return __pbn__FromUid.HasValue;
	}

	public void ResetFromUid()
	{
		__pbn__FromUid = null;
	}

	public bool ShouldSerializeCostHp()
	{
		return __pbn__CostHp.HasValue;
	}

	public void ResetCostHp()
	{
		__pbn__CostHp = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}
}
