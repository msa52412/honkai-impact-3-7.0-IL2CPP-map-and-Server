using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeDutyRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 289
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "DUTY_POINT_LACK")]
		DutyPointLack
	}

	private IExtension __pbn__extensionData;

	private DutyType? __pbn__DutyType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "duty_type")]
	[DefaultValue(DutyType.DutyDaily)]
	public DutyType DutyType
	{
		get
		{
			return __pbn__DutyType ?? DutyType.DutyDaily;
		}
		set
		{
			__pbn__DutyType = value;
		}
	}

	[ProtoMember(3, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(4, Name = "duty_id_list")]
	public uint[] DutyIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDutyType()
	{
		return __pbn__DutyType.HasValue;
	}

	public void ResetDutyType()
	{
		__pbn__DutyType = null;
	}
}
