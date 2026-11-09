using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddGodWarRoleRelationExpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6177
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__RoleId;

	private uint? __pbn__ExpMaterialNum;

	[ProtoMember(1, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(2, Name = "role_id")]
	public uint RoleId
	{
		get
		{
			return __pbn__RoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__RoleId = value;
		}
	}

	[ProtoMember(3, Name = "exp_material_num")]
	public uint ExpMaterialNum
	{
		get
		{
			return __pbn__ExpMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpMaterialNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeRoleId()
	{
		return __pbn__RoleId.HasValue;
	}

	public void ResetRoleId()
	{
		__pbn__RoleId = null;
	}

	public bool ShouldSerializeExpMaterialNum()
	{
		return __pbn__ExpMaterialNum.HasValue;
	}

	public void ResetExpMaterialNum()
	{
		__pbn__ExpMaterialNum = null;
	}
}
