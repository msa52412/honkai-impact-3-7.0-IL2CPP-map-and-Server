using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBeastChallengeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2461
	}

	private IExtension __pbn__extensionData;

	private ulong? __pbn__BeastUniqueIdU64;

	private uint? __pbn__BeastId;

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

	[ProtoMember(2, Name = "beast_id")]
	public uint BeastId
	{
		get
		{
			return __pbn__BeastId.GetValueOrDefault();
		}
		set
		{
			__pbn__BeastId = value;
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

	public bool ShouldSerializeBeastId()
	{
		return __pbn__BeastId.HasValue;
	}

	public void ResetBeastId()
	{
		__pbn__BeastId = null;
	}
}
