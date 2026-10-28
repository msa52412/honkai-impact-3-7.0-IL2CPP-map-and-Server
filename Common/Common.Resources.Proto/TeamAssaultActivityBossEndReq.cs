using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TeamAssaultActivityBossEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4026
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BossIdx;

	private uint? __pbn__PassTime;

	private float? __pbn__HpPercentage;

	private string __pbn__ClientData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "boss_idx")]
	public uint BossIdx
	{
		get
		{
			return __pbn__BossIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__BossIdx = value;
		}
	}

	[ProtoMember(3, Name = "pass_time")]
	public uint PassTime
	{
		get
		{
			return __pbn__PassTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PassTime = value;
		}
	}

	[ProtoMember(4, Name = "hp_percentage")]
	public float HpPercentage
	{
		get
		{
			return __pbn__HpPercentage.GetValueOrDefault();
		}
		set
		{
			__pbn__HpPercentage = value;
		}
	}

	[ProtoMember(6, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


	[ProtoMember(7, Name = "client_data")]
	[DefaultValue("")]
	public string ClientData
	{
		get
		{
			return __pbn__ClientData ?? "";
		}
		set
		{
			__pbn__ClientData = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeBossIdx()
	{
		return __pbn__BossIdx.HasValue;
	}

	public void ResetBossIdx()
	{
		__pbn__BossIdx = null;
	}

	public bool ShouldSerializePassTime()
	{
		return __pbn__PassTime.HasValue;
	}

	public void ResetPassTime()
	{
		__pbn__PassTime = null;
	}

	public bool ShouldSerializeHpPercentage()
	{
		return __pbn__HpPercentage.HasValue;
	}

	public void ResetHpPercentage()
	{
		__pbn__HpPercentage = null;
	}

	public bool ShouldSerializeClientData()
	{
		return __pbn__ClientData != null;
	}

	public void ResetClientData()
	{
		__pbn__ClientData = null;
	}
}
