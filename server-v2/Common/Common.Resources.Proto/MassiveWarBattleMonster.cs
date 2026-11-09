using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarBattleMonster : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MessageId;

	private uint? __pbn__MonsterId;

	private uint? __pbn__TotalHp;

	private uint? __pbn__LeftHp;

	[ProtoMember(1, Name = "message_id")]
	public uint MessageId
	{
		get
		{
			return __pbn__MessageId.GetValueOrDefault();
		}
		set
		{
			__pbn__MessageId = value;
		}
	}

	[ProtoMember(2, Name = "monster_id")]
	public uint MonsterId
	{
		get
		{
			return __pbn__MonsterId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterId = value;
		}
	}

	[ProtoMember(3, Name = "total_hp")]
	public uint TotalHp
	{
		get
		{
			return __pbn__TotalHp.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalHp = value;
		}
	}

	[ProtoMember(4, Name = "left_hp")]
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

	public bool ShouldSerializeMessageId()
	{
		return __pbn__MessageId.HasValue;
	}

	public void ResetMessageId()
	{
		__pbn__MessageId = null;
	}

	public bool ShouldSerializeMonsterId()
	{
		return __pbn__MonsterId.HasValue;
	}

	public void ResetMonsterId()
	{
		__pbn__MonsterId = null;
	}

	public bool ShouldSerializeTotalHp()
	{
		return __pbn__TotalHp.HasValue;
	}

	public void ResetTotalHp()
	{
		__pbn__TotalHp = null;
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
