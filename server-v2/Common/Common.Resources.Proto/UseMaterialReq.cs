using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UseMaterialReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 251
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__Num;

	private uint? __pbn__Parameter;

	private uint? __pbn__SimulateMagic;

	[ProtoMember(1, Name = "material_id")]
	public uint MaterialId
	{
		get
		{
			return __pbn__MaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialId = value;
		}
	}

	[ProtoMember(2, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	[ProtoMember(3, Name = "parameter")]
	public uint Parameter
	{
		get
		{
			return __pbn__Parameter.GetValueOrDefault();
		}
		set
		{
			__pbn__Parameter = value;
		}
	}

	[ProtoMember(4, Name = "simulate_magic")]
	public uint SimulateMagic
	{
		get
		{
			return __pbn__SimulateMagic.GetValueOrDefault();
		}
		set
		{
			__pbn__SimulateMagic = value;
		}
	}

	[ProtoMember(5, Name = "consume_item_list")]
	public EquipmentItemList ConsumeItemList { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeParameter()
	{
		return __pbn__Parameter.HasValue;
	}

	public void ResetParameter()
	{
		__pbn__Parameter = null;
	}

	public bool ShouldSerializeSimulateMagic()
	{
		return __pbn__SimulateMagic.HasValue;
	}

	public void ResetSimulateMagic()
	{
		__pbn__SimulateMagic = null;
	}
}
