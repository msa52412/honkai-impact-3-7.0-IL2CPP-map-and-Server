using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RefreshDLCTalentAffixRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3581
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError,
		[ProtoEnum(Name = "NOT_REFRESHABLE_TALENT")]
		NotRefreshableTalent,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "WAIT_SELECT_AFFIX_SET_NOT_EMPTY")]
		WaitSelectAffixSetNotEmpty
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NewAffixSetId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "new_affix_set_id")]
	public uint NewAffixSetId
	{
		get
		{
			return __pbn__NewAffixSetId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewAffixSetId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNewAffixSetId()
	{
		return __pbn__NewAffixSetId.HasValue;
	}

	public void ResetNewAffixSetId()
	{
		__pbn__NewAffixSetId = null;
	}
}
