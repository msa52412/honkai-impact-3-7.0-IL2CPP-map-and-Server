using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpArmadaCabinReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2631
	}

	private IExtension __pbn__extensionData;

	private ArmadaCabinType? __pbn__Type;

	private uint? __pbn__CabinId;

	private uint? __pbn__GridId;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ArmadaCabinType.ArmadaCabinNone)]
	public ArmadaCabinType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "cabin_id")]
	public uint CabinId
	{
		get
		{
			return __pbn__CabinId.GetValueOrDefault();
		}
		set
		{
			__pbn__CabinId = value;
		}
	}

	[ProtoMember(3, Name = "grid_id")]
	public uint GridId
	{
		get
		{
			return __pbn__GridId.GetValueOrDefault();
		}
		set
		{
			__pbn__GridId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeCabinId()
	{
		return __pbn__CabinId.HasValue;
	}

	public void ResetCabinId()
	{
		__pbn__CabinId = null;
	}

	public bool ShouldSerializeGridId()
	{
		return __pbn__GridId.HasValue;
	}

	public void ResetGridId()
	{
		__pbn__GridId = null;
	}
}
