using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarNpc : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__NpcId;

	[ProtoMember(1, Name = "npc_id")]
	public uint NpcId
	{
		get
		{
			return __pbn__NpcId.GetValueOrDefault();
		}
		set
		{
			__pbn__NpcId = value;
		}
	}

	[ProtoMember(2, Name = "action_list")]
	public uint[] ActionLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNpcId()
	{
		return __pbn__NpcId.HasValue;
	}

	public void ResetNpcId()
	{
		__pbn__NpcId = null;
	}
}
