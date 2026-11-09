using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetAvatarEquipSuiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1457
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_LOCK")]
		AvatarLock,
		[ProtoEnum(Name = "SLOT_WEAPON")]
		SlotWeapon,
		[ProtoEnum(Name = "SLOT_STIGMATA_1")]
		SlotStigmata1,
		[ProtoEnum(Name = "SLOT_STIGMATA_2")]
		SlotStigmata2,
		[ProtoEnum(Name = "SLOT_STIGMATA_3")]
		SlotStigmata3,
		[ProtoEnum(Name = "SUITE_ID")]
		SuiteId,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "NAME_LEN_ERROR")]
		NameLenError,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent,
		[ProtoEnum(Name = "FORMAT_ERROR")]
		FormatError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SuiteId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "suite_id")]
	public uint SuiteId
	{
		get
		{
			return __pbn__SuiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SuiteId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSuiteId()
	{
		return __pbn__SuiteId.HasValue;
	}

	public void ResetSuiteId()
	{
		__pbn__SuiteId = null;
	}
}
