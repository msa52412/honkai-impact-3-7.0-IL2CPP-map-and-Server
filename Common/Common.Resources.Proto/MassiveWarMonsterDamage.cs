using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarMonsterDamage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MessageId;

	private uint? __pbn__TotalDamage;

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

	[ProtoMember(2, Name = "total_damage")]
	public uint TotalDamage
	{
		get
		{
			return __pbn__TotalDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalDamage = value;
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

	public bool ShouldSerializeTotalDamage()
	{
		return __pbn__TotalDamage.HasValue;
	}

	public void ResetTotalDamage()
	{
		__pbn__TotalDamage = null;
	}
}
