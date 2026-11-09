using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldMechaDefenseEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4512
	}

	private IExtension __pbn__extensionData;

	private StageEndStatus? __pbn__EndStatus;

	private uint? __pbn__HardLevel;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "end_status")]
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

	[ProtoMember(2, Name = "hard_level")]
	public uint HardLevel
	{
		get
		{
			return __pbn__HardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HardLevel = value;
		}
	}

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(4, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
