using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectDLCTalentAffixReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3584
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__TalentId;

	private bool? __pbn__IsSelect;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
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

	[ProtoMember(3, Name = "is_select")]
	public bool IsSelect
	{
		get
		{
			return __pbn__IsSelect.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSelect = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeTalentId()
	{
		return __pbn__TalentId.HasValue;
	}

	public void ResetTalentId()
	{
		__pbn__TalentId = null;
	}

	public bool ShouldSerializeIsSelect()
	{
		return __pbn__IsSelect.HasValue;
	}

	public void ResetIsSelect()
	{
		__pbn__IsSelect = null;
	}
}
