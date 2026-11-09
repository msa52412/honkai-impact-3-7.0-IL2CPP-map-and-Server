using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterMoonRelicsChangeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4687
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__RelicsId;

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

	[ProtoMember(2, Name = "relics_id")]
	public uint RelicsId
	{
		get
		{
			return __pbn__RelicsId.GetValueOrDefault();
		}
		set
		{
			__pbn__RelicsId = value;
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

	public bool ShouldSerializeRelicsId()
	{
		return __pbn__RelicsId.HasValue;
	}

	public void ResetRelicsId()
	{
		__pbn__RelicsId = null;
	}
}
[ProtoContract]
public class OpenworldChapterMoonRelicsChangeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4688
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 1,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FRAGMENT_LACK")]
		FragmentLack,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "MAP_MISMATCH")]
		MapMismatch,
		[ProtoEnum(Name = "RELICS_EXIST")]
		RelicsExist
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__RelicsId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; } = Retcode.Succ;


	[ProtoMember(2, Name = "map_id")]
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

	[ProtoMember(3, Name = "relics_id")]
	public uint RelicsId
	{
		get
		{
			return __pbn__RelicsId.GetValueOrDefault();
		}
		set
		{
			__pbn__RelicsId = value;
		}
	}

	[ProtoMember(4, Name = "reward")]
	public RewardData Reward { get; set; }

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

	public bool ShouldSerializeRelicsId()
	{
		return __pbn__RelicsId.HasValue;
	}

	public void ResetRelicsId()
	{
		__pbn__RelicsId = null;
	}
}
