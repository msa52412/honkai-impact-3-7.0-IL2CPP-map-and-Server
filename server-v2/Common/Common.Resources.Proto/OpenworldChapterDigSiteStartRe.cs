using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterDigSiteStartReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4665
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__MainId;

	private uint? __pbn__ProgramId;

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

	[ProtoMember(3, Name = "program_id")]
	public uint ProgramId
	{
		get
		{
			return __pbn__ProgramId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgramId = value;
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

	public bool ShouldSerializeProgramId()
	{
		return __pbn__ProgramId.HasValue;
	}

	public void ResetProgramId()
	{
		__pbn__ProgramId = null;
	}
}
