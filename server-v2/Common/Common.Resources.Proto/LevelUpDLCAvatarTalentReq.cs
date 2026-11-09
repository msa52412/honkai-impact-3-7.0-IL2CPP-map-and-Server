using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpDLCAvatarTalentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3552
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TalentId;

	private uint? __pbn__TargetLevel;

	[ProtoMember(1, Name = "talent_id")]
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

	[ProtoMember(2, Name = "target_level")]
	public uint TargetLevel
	{
		get
		{
			return __pbn__TargetLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTalentId()
	{
		return __pbn__TalentId.HasValue;
	}

	public void ResetTalentId()
	{
		__pbn__TalentId = null;
	}

	public bool ShouldSerializeTargetLevel()
	{
		return __pbn__TargetLevel.HasValue;
	}

	public void ResetTargetLevel()
	{
		__pbn__TargetLevel = null;
	}
}
