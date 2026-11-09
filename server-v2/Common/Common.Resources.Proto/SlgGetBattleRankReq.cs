using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgGetBattleRankReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6062
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CountryId;

	[ProtoMember(1, Name = "country_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCountryId()
	{
		return __pbn__CountryId.HasValue;
	}

	public void ResetCountryId()
	{
		__pbn__CountryId = null;
	}
}
