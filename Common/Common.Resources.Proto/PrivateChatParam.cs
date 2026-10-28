using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PrivateChatParam : IExtensible
{
	private IExtension __pbn__extensionData;

	private ChatRelationType? __pbn__RelationType;

	[ProtoMember(1, Name = "relation_type")]
	[DefaultValue(ChatRelationType.ChatRelationTypeNone)]
	public ChatRelationType RelationType
	{
		get
		{
			return __pbn__RelationType.GetValueOrDefault();
		}
		set
		{
			__pbn__RelationType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRelationType()
	{
		return __pbn__RelationType.HasValue;
	}

	public void ResetRelationType()
	{
		__pbn__RelationType = null;
	}
}
