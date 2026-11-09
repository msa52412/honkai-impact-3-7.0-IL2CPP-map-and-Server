using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GratuityStageMessage : IExtensible
{
	[ProtoContract]
	public class MonsterDamage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__MonsterId;

		private uint? __pbn__CostHp;

		[ProtoMember(1, Name = "monster_id")]
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

		public bool ShouldSerializeMonsterId()
		{
			return __pbn__MonsterId.HasValue;
		}

		public void ResetMonsterId()
		{
			__pbn__MonsterId = null;
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

	[ProtoContract]
	public enum MessageType
	{
		[ProtoEnum(Name = "MESSAGE_NONE")]
		MessageNone,
		[ProtoEnum(Name = "MESSAGE_MONSTER_DAMAGE")]
		MessageMonsterDamage
	}

	private IExtension __pbn__extensionData;

	private MessageType? __pbn__message_type;

	private uint? __pbn__FromUid;

	[ProtoMember(1)]
	[DefaultValue(MessageType.MessageNone)]
	public MessageType message_type
	{
		get
		{
			return __pbn__message_type.GetValueOrDefault();
		}
		set
		{
			__pbn__message_type = value;
		}
	}

	[ProtoMember(2, Name = "from_uid")]
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

	[ProtoMember(3, Name = "monster_damage_list")]
	public List<MonsterDamage> MonsterDamageLists { get; } = new List<MonsterDamage>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializemessage_type()
	{
		return __pbn__message_type.HasValue;
	}

	public void Resetmessage_type()
	{
		__pbn__message_type = null;
	}

	public bool ShouldSerializeFromUid()
	{
		return __pbn__FromUid.HasValue;
	}

	public void ResetFromUid()
	{
		__pbn__FromUid = null;
	}
}
