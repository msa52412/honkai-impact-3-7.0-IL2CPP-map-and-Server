using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelAttackSnapshot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Time;

	[ProtoMember(1, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	[ProtoMember(2, Name = "attack_data_created")]
	public InLevelAttackDataSnapshot AttackDataCreated { get; set; }

	[ProtoMember(3, Name = "attack_data_resoved")]
	public InLevelAttackDataSnapshot AttackDataResoved { get; set; }

	[ProtoMember(4, Name = "actor_list")]
	public List<InLevelActorSnapshot> ActorLists { get; } = new List<InLevelActorSnapshot>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}
}
