using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddScDLCSupportNPCExpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6308
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NpcId;

	private uint? __pbn__MaterialId;

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

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}
}
