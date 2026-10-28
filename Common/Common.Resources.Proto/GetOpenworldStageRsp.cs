using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOpenworldStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4461
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CYCLE_NOT_OPEN")]
		CycleNotOpen,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__MechaLostHpPercent;

	private uint? __pbn__MechaLostSpPercent;

	private uint? __pbn__MapEnergy;

	private uint? __pbn__ScDlcFeverScore;

	private uint? __pbn__ScDlcClimaxScore;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "avatar_list")]
	public List<OpenworldStageAvatarInfo> AvatarLists { get; } = new List<OpenworldStageAvatarInfo>();


	[ProtoMember(3, Name = "cook_buffer_list")]
	public uint[] CookBufferLists { get; set; }

	[ProtoMember(4, Name = "map_id")]
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

	[ProtoMember(5, Name = "mecha_buff_list")]
	public uint[] MechaBuffLists { get; set; }

	[ProtoMember(6, Name = "mecha_lost_hp_percent")]
	public uint MechaLostHpPercent
	{
		get
		{
			return __pbn__MechaLostHpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaLostHpPercent = value;
		}
	}

	[ProtoMember(7, Name = "mecha_lost_sp_percent")]
	public uint MechaLostSpPercent
	{
		get
		{
			return __pbn__MechaLostSpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaLostSpPercent = value;
		}
	}

	[ProtoMember(8, Name = "map_energy")]
	public uint MapEnergy
	{
		get
		{
			return __pbn__MapEnergy.GetValueOrDefault();
		}
		set
		{
			__pbn__MapEnergy = value;
		}
	}

	[ProtoMember(9, Name = "sc_dlc_fever_score")]
	public uint ScDlcFeverScore
	{
		get
		{
			return __pbn__ScDlcFeverScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcFeverScore = value;
		}
	}

	[ProtoMember(10, Name = "sc_dlc_climax_score")]
	public uint ScDlcClimaxScore
	{
		get
		{
			return __pbn__ScDlcClimaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcClimaxScore = value;
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

	public bool ShouldSerializeMechaLostHpPercent()
	{
		return __pbn__MechaLostHpPercent.HasValue;
	}

	public void ResetMechaLostHpPercent()
	{
		__pbn__MechaLostHpPercent = null;
	}

	public bool ShouldSerializeMechaLostSpPercent()
	{
		return __pbn__MechaLostSpPercent.HasValue;
	}

	public void ResetMechaLostSpPercent()
	{
		__pbn__MechaLostSpPercent = null;
	}

	public bool ShouldSerializeMapEnergy()
	{
		return __pbn__MapEnergy.HasValue;
	}

	public void ResetMapEnergy()
	{
		__pbn__MapEnergy = null;
	}

	public bool ShouldSerializeScDlcFeverScore()
	{
		return __pbn__ScDlcFeverScore.HasValue;
	}

	public void ResetScDlcFeverScore()
	{
		__pbn__ScDlcFeverScore = null;
	}

	public bool ShouldSerializeScDlcClimaxScore()
	{
		return __pbn__ScDlcClimaxScore.HasValue;
	}

	public void ResetScDlcClimaxScore()
	{
		__pbn__ScDlcClimaxScore = null;
	}
}
