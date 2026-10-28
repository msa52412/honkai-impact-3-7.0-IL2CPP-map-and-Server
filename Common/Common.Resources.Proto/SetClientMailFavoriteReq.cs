using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetClientMailFavoriteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3809
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsFavorite;

	[ProtoMember(1, Name = "mail_key")]
	public ClientMailKey MailKey { get; set; }

	[ProtoMember(2, Name = "is_favorite")]
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
