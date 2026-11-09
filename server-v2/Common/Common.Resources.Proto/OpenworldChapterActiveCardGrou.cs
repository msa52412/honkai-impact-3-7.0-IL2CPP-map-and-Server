using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterActiveCardGroupReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4589
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__GroupIdx;

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
}
[ProtoContract]
public class OpenworldChapterActiveCardGroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4590
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
