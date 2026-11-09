using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldHuntActivityChangeHunterAreaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4611
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__HunterId;

	private string __pbn__SpawnPoint;

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

	[ProtoMember(2, Name = "hunter_id")]
	public uint HunterId
	{
		get
		{
			return __pbn__HunterId.GetValueOrDefault();
		}
		set
		{
			__pbn__HunterId = value;
		}
	}

	[ProtoMember(3, Name = "spawn_point")]
	[DefaultValue("")]
	public string SpawnPoint
	{
		get
		{
			return __pbn__SpawnPoint ?? "";
		}
		set
		{
			__pbn__SpawnPoint = value;
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

	public bool ShouldSerializeHunterId()
	{
		return __pbn__HunterId.HasValue;
	}

	public void ResetHunterId()
	{
		__pbn__HunterId = null;
	}

	public bool ShouldSerializeSpawnPoint()
	{
		return __pbn__SpawnPoint != null;
	}

	public void ResetSpawnPoint()
	{
		__pbn__SpawnPoint = null;
	}
}
[ProtoContract]
public class OpenworldHuntActivityChangeHunterAreaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4612
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "NOT_IN_MAP")]
		NotInMap = 3,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError = 4,
		[ProtoEnum(Name = "TARGET_AREA_ERROR")]
		TargetAreaError = 5,
		[ProtoEnum(Name = "HUNTER_ERROR")]
		HunterError = 6
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
