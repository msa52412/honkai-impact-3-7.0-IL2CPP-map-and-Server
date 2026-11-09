using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBroadcastPlayer : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__CountryId;

	private string __pbn__Nickname;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "country_id")]
	public uint CountryId
	{
		get
		{
			return __pbn__CountryId.GetValueOrDefault();
		}
		set
		{
			__pbn__CountryId = value;
		}
	}

	[ProtoMember(3, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeCountryId()
	{
		return __pbn__CountryId.HasValue;
	}

	public void ResetCountryId()
	{
		__pbn__CountryId = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}
}
