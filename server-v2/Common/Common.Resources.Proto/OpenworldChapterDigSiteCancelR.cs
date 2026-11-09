using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterDigSiteCancelReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4667
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__MainId;

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
}
[ProtoContract]
public class OpenworldChapterDigSiteCancelRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4668
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
		[ProtoEnum(Name = "NOT_IN_PROGRESS")]
		NotInProgress,
		[ProtoEnum(Name = "BUILDING_ERROR")]
		BuildingError,
		[ProtoEnum(Name = "PROGRAM_FINISHED")]
		ProgramFinished
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
