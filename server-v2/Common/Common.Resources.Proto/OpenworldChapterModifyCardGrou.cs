using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterModifyCardGroupNameReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4587
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__GroupIdx;

	private string __pbn__GroupName;

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

	[ProtoMember(2, Name = "group_idx")]
	public uint GroupIdx
	{
		get
		{
			return __pbn__GroupIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupIdx = value;
		}
	}

	[ProtoMember(3, Name = "group_name")]
	[DefaultValue("")]
	public string GroupName
	{
		get
		{
			return __pbn__GroupName ?? "";
		}
		set
		{
			__pbn__GroupName = value;
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

	public bool ShouldSerializeGroupIdx()
	{
		return __pbn__GroupIdx.HasValue;
	}

	public void ResetGroupIdx()
	{
		__pbn__GroupIdx = null;
	}

	public bool ShouldSerializeGroupName()
	{
		return __pbn__GroupName != null;
	}

	public void ResetGroupName()
	{
		__pbn__GroupName = null;
	}
}
[ProtoContract]
public class OpenworldChapterModifyCardGroupNameRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4588
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FORMAT_ERROR")]
		FormatError,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "GROUP_ERROR")]
		GroupError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
