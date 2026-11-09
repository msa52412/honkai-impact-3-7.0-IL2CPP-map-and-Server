using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReunionCookMakeDinnerRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4383
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "INVALID_BOOK_ID")]
		InvalidBookId,
		[ProtoEnum(Name = "INVALID_MATERIAL")]
		InvalidMaterial,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "TIMES_LIMIT")]
		TimesLimit,
		[ProtoEnum(Name = "DUPLICATE_BOOK_ID")]
		DuplicateBookId
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BookId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "book_id")]
	public uint BookId
	{
		get
		{
			return __pbn__BookId.GetValueOrDefault();
		}
		set
		{
			__pbn__BookId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBookId()
	{
		return __pbn__BookId.HasValue;
	}

	public void ResetBookId()
	{
		__pbn__BookId = null;
	}
}
