using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFishEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2435
	}

	[ProtoContract]
	public enum FishEndStatus
	{
		[ProtoEnum(Name = "FISH_END_SUCC")]
		FishEndSucc,
		[ProtoEnum(Name = "FISH_END_WAIT_ASSIST")]
		FishEndWaitAssist,
		[ProtoEnum(Name = "FISH_END_FAIL")]
		FishEndFail
	}

	private IExtension __pbn__extensionData;

	private FishEndStatus? __pbn__fish_end_status;

	private uint? __pbn__FishSpotId;

	private uint? __pbn__FishGroupId;

	private uint? __pbn__FishHardLevel;

	[ProtoMember(1)]
	[DefaultValue(FishEndStatus.FishEndSucc)]
	public FishEndStatus fish_end_status
	{
		get
		{
			return __pbn__fish_end_status.GetValueOrDefault();
		}
		set
		{
			__pbn__fish_end_status = value;
		}
	}

	[ProtoMember(2, Name = "fish_spot_id")]
	public uint FishSpotId
	{
		get
		{
			return __pbn__FishSpotId.GetValueOrDefault();
		}
		set
		{
			__pbn__FishSpotId = value;
		}
	}

	[ProtoMember(3, Name = "fish_group_id")]
	public uint FishGroupId
	{
		get
		{
			return __pbn__FishGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__FishGroupId = value;
		}
	}

	[ProtoMember(4, Name = "fish_hard_level")]
	public uint FishHardLevel
	{
		get
		{
			return __pbn__FishHardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__FishHardLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializefish_end_status()
	{
		return __pbn__fish_end_status.HasValue;
	}

	public void Resetfish_end_status()
	{
		__pbn__fish_end_status = null;
	}

	public bool ShouldSerializeFishSpotId()
	{
		return __pbn__FishSpotId.HasValue;
	}

	public void ResetFishSpotId()
	{
		__pbn__FishSpotId = null;
	}

	public bool ShouldSerializeFishGroupId()
	{
		return __pbn__FishGroupId.HasValue;
	}

	public void ResetFishGroupId()
	{
		__pbn__FishGroupId = null;
	}

	public bool ShouldSerializeFishHardLevel()
	{
		return __pbn__FishHardLevel.HasValue;
	}

	public void ResetFishHardLevel()
	{
		__pbn__FishHardLevel = null;
	}
}
