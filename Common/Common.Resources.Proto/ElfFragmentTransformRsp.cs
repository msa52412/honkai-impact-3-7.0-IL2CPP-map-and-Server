using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfFragmentTransformRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2122
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STAR_LACK")]
		StarLack,
		[ProtoEnum(Name = "FRAGMENT_LACK")]
		FragmentLack,
		[ProtoEnum(Name = "MATERIAL_FULL")]
		MaterialFull,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AddMaterialId;

	private uint? __pbn__AddMaterialNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "add_material_id")]
	public uint AddMaterialId
	{
		get
		{
			return __pbn__AddMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__AddMaterialId = value;
		}
	}

	[ProtoMember(3, Name = "add_material_num")]
	public uint AddMaterialNum
	{
		get
		{
			return __pbn__AddMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AddMaterialNum = value;
		}
	}

	[ProtoMember(4, Name = "return_list")]
	public List<GenericItemNum> ReturnLists { get; } = new List<GenericItemNum>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddMaterialId()
	{
		return __pbn__AddMaterialId.HasValue;
	}

	public void ResetAddMaterialId()
	{
		__pbn__AddMaterialId = null;
	}

	public bool ShouldSerializeAddMaterialNum()
	{
		return __pbn__AddMaterialNum.HasValue;
	}

	public void ResetAddMaterialNum()
	{
		__pbn__AddMaterialNum = null;
	}
}
