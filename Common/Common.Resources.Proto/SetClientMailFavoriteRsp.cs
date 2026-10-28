using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetClientMailFavoriteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3810
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "DAILY_LIMIT")]
		DailyLimit
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsFavorite;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "mail_key")]
	public ClientMailKey MailKey { get; set; }

	[ProtoMember(3, Name = "is_favorite")]
	public bool IsFavorite
	{
		get
		{
			return __pbn__IsFavorite.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFavorite = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsFavorite()
	{
		return __pbn__IsFavorite.HasValue;
	}

	public void ResetIsFavorite()
	{
		__pbn__IsFavorite = null;
	}
}
