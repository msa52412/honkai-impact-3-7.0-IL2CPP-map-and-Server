using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddTechExpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 538
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__MaterialNum;

	private uint? __pbn__MapId;

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

	[ProtoMember(2, Name = "material_num")]
	public uint MaterialNum
	{
		get
		{
			return __pbn__MaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialNum = value;
		}
	}

	[ProtoMember(3, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
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

	public bool ShouldSerializeMaterialNum()
	{
		return __pbn__MaterialNum.HasValue;
	}

	public void ResetMaterialNum()
	{
		__pbn__MaterialNum = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
