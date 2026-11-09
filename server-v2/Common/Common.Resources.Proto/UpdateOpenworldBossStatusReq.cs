using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateOpenworldBossStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4522
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__LeftHp;

	private uint? __pbn__BattleTime;

	private uint? __pbn__LocationIdx;

	private string __pbn__ReportExt;

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

	[ProtoMember(2, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(3, Name = "battle_time")]
	public uint BattleTime
	{
		get
		{
			return __pbn__BattleTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleTime = value;
		}
	}

	[ProtoMember(4, Name = "location_idx")]
	public uint LocationIdx
	{
		get
		{
			return __pbn__LocationIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__LocationIdx = value;
		}
	}

	[ProtoMember(5, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
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

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeBattleTime()
	{
		return __pbn__BattleTime.HasValue;
	}

	public void ResetBattleTime()
	{
		__pbn__BattleTime = null;
	}

	public bool ShouldSerializeLocationIdx()
	{
		return __pbn__LocationIdx.HasValue;
	}

	public void ResetLocationIdx()
	{
		__pbn__LocationIdx = null;
	}

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
