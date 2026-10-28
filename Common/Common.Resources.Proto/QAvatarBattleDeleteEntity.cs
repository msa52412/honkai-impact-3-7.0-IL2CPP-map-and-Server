using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleDeleteEntity : IExtensible
{
	private IExtension __pbn__extensionData;

	private QAvatarBattleEntityDeleteReasonType? __pbn__DeleteReason;

	private uint? __pbn__EntityId;

	[ProtoMember(1, Name = "delete_reason")]
	[DefaultValue(QAvatarBattleEntityDeleteReasonType.QavatarBattleEntityDeleteReasonNone)]
	public QAvatarBattleEntityDeleteReasonType DeleteReason
	{
		get
		{
			return __pbn__DeleteReason.GetValueOrDefault();
		}
		set
		{
			__pbn__DeleteReason = value;
		}
	}

	[ProtoMember(2, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDeleteReason()
	{
		return __pbn__DeleteReason.HasValue;
	}

	public void ResetDeleteReason()
	{
		__pbn__DeleteReason = null;
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}
}
