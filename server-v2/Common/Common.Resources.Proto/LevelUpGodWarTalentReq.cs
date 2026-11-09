using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpGodWarTalentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6172
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TalentId;

	private bool? __pbn__IsLevelUpAll;

	[ProtoMember(1, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(2, Name = "talent_id")]
	public uint TalentId
	{
		get
		{
			return __pbn__TalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__TalentId = value;
		}
	}

	[ProtoMember(3, Name = "is_level_up_all")]
	public bool IsLevelUpAll
	{
		get
		{
			return __pbn__IsLevelUpAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLevelUpAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeTalentId()
	{
		return __pbn__TalentId.HasValue;
	}

	public void ResetTalentId()
	{
		__pbn__TalentId = null;
	}

	public bool ShouldSerializeIsLevelUpAll()
	{
		return __pbn__IsLevelUpAll.HasValue;
	}

	public void ResetIsLevelUpAll()
	{
		__pbn__IsLevelUpAll = null;
	}
}
