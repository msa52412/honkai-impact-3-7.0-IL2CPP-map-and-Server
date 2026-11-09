using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityMaterialUseReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6470
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__TargetParam;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "material_id")]
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

	[ProtoMember(3, Name = "target_param")]
	public uint TargetParam
	{
		get
		{
			return __pbn__TargetParam.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetParam = value;
		}
	}

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

	public bool ShouldSerializeTargetParam()
	{
		return __pbn__TargetParam.HasValue;
	}

	public void ResetTargetParam()
	{
		__pbn__TargetParam = null;
	}
}
