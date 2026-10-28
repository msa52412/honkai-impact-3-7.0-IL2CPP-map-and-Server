using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarBattleMessage : IExtensible
{
	[ProtoContract]
	public class DamageMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private ulong? __pbn__AddDamage;

		private ulong? __pbn__TotalDamage;

		[ProtoMember(1, Name = "add_damage")]
		public ulong AddDamage
		{
			get
			{
				return __pbn__AddDamage.GetValueOrDefault();
			}
			set
			{
				__pbn__AddDamage = value;
			}
		}

		[ProtoMember(2, Name = "total_damage")]
		public ulong TotalDamage
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

		public bool ShouldSerializeAddDamage()
		{
			return __pbn__AddDamage.HasValue;
		}

		public void ResetAddDamage()
		{
			__pbn__AddDamage = null;
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

	[ProtoContract]
	public class FieldMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private MassiveWarFieldActionType? __pbn__ActionType;

		private uint? __pbn__BuffProgress;

		[ProtoMember(1, Name = "action_type")]
		[DefaultValue(MassiveWarFieldActionType.MassiveWarFieldActionNone)]
		public MassiveWarFieldActionType ActionType
		{
			get
			{
				return __pbn__ActionType.GetValueOrDefault();
			}
			set
			{
				__pbn__ActionType = value;
			}
		}

		[ProtoMember(2, Name = "buff_progress")]
		public uint BuffProgress
		{
			get
			{
				return __pbn__BuffProgress.GetValueOrDefault();
			}
			set
			{
				__pbn__BuffProgress = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeActionType()
		{
			return __pbn__ActionType.HasValue;
		}

		public void ResetActionType()
		{
			__pbn__ActionType = null;
		}

		public bool ShouldSerializeBuffProgress()
		{
			return __pbn__BuffProgress.HasValue;
		}

		public void ResetBuffProgress()
		{
			__pbn__BuffProgress = null;
		}
	}

	[ProtoContract]
	public enum MassiveWarMessageType
	{
		[ProtoEnum(Name = "MASSIVE_WAR_MESSAGE_NONE")]
		MassiveWarMessageNone,
		[ProtoEnum(Name = "MASSIVE_WAR_MESSAGE_BOSS_DAMAGE")]
		MassiveWarMessageBossDamage,
		[ProtoEnum(Name = "MASSIVE_WAR_MESSAGE_FIELD")]
		MassiveWarMessageField
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MessageId;

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

	[ProtoMember(11, Name = "damage_msg")]
	public DamageMessage DamageMsg { get; set; }

	[ProtoMember(12, Name = "field_msg")]
	public FieldMessage FieldMsg { get; set; }

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
}
