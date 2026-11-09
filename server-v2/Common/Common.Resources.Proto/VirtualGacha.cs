using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualGacha : IExtensible
{
	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "NORMAL")]
		Normal,
		[ProtoEnum(Name = "EMPTY")]
		Empty
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BoxId;

	private Status? __pbn__status;

	[ProtoMember(1, Name = "box_id")]
	public uint BoxId
	{
		get
		{
			return __pbn__BoxId.GetValueOrDefault();
		}
		set
		{
			__pbn__BoxId = value;
		}
	}

	[ProtoMember(2)]
	[DefaultValue(Status.Normal)]
	public Status status
	{
		get
		{
			return __pbn__status.GetValueOrDefault();
		}
		set
		{
			__pbn__status = value;
		}
	}

	[ProtoMember(3, Name = "pool_list")]
	public List<VirtualGachaPool> PoolLists { get; } = new List<VirtualGachaPool>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBoxId()
	{
		return __pbn__BoxId.HasValue;
	}

	public void ResetBoxId()
	{
		__pbn__BoxId = null;
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}
}
