using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaStageEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2671
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BossDamage;

	private StageEndStatus? __pbn__EndStatus;

	private bool? __pbn__IsFightBody;

	[ProtoMember(1, Name = "boss_damage")]
	public uint BossDamage
	{
		get
		{
			return __pbn__BossDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__BossDamage = value;
		}
	}

	[ProtoMember(2, Name = "end_status")]
	[DefaultValue(StageEndStatus.StageWin)]
	public StageEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? StageEndStatus.StageWin;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(4, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


	[ProtoMember(5, Name = "is_fight_body")]
	public bool IsFightBody
	{
		get
		{
			return __pbn__IsFightBody.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFightBody = value;
		}
	}

	[ProtoMember(6, Name = "body")]
	public StageEndReqBody Body { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossDamage()
	{
		return __pbn__BossDamage.HasValue;
	}

	public void ResetBossDamage()
	{
		__pbn__BossDamage = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeIsFightBody()
	{
		return __pbn__IsFightBody.HasValue;
	}

	public void ResetIsFightBody()
	{
		__pbn__IsFightBody = null;
	}
}
