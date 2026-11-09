using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterDigSiteAccelerateReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4671
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__MainId;

	private uint? __pbn__MaterialId;

	private uint? __pbn__MaterialNum;

	[ProtoMember(1, Name = "map_id")]
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

	[ProtoMember(2, Name = "main_id")]
	public uint MainId
	{
		get
		{
			return __pbn__MainId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainId = value;
		}
	}

	[ProtoMember(3, Name = "material_id")]
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

	[ProtoMember(4, Name = "material_num")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeMainId()
	{
		return __pbn__MainId.HasValue;
	}

	public void ResetMainId()
	{
		__pbn__MainId = null;
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
}
[ProtoContract]
public class OpenworldChapterDigSiteAccelerateRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4672
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "BUILDING_ERROR")]
		BuildingError,
		[ProtoEnum(Name = "NOT_IN_PROGRESS")]
		NotInProgress,
		[ProtoEnum(Name = "PROGRAM_FINISHED")]
		ProgramFinished,
		[ProtoEnum(Name = "MATERIAL_ERROR")]
		MaterialError,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
