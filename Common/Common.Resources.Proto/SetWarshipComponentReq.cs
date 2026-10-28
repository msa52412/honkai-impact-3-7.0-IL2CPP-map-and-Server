using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetWarshipComponentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5458
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WarshipId;

	[ProtoMember(1, Name = "warship_id")]
	public uint WarshipId
	{
		get
		{
			return __pbn__WarshipId.GetValueOrDefault();
		}
		set
		{
			__pbn__WarshipId = value;
		}
	}

	[ProtoMember(2, Name = "component_list")]
	public List<WarshipComponent> ComponentLists { get; } = new List<WarshipComponent>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWarshipId()
	{
		return __pbn__WarshipId.HasValue;
	}

	public void ResetWarshipId()
	{
		__pbn__WarshipId = null;
	}
}
