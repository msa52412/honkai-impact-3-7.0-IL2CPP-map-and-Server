using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgGetDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6050
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CountryId;

	private string __pbn__Region;

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

	[ProtoMember(2, Name = "region")]
	[DefaultValue("")]
	public string Region
	{
		get
		{
			return __pbn__Region ?? "";
		}
		set
		{
			__pbn__Region = value;
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

	public bool ShouldSerializeRegion()
	{
		return __pbn__Region != null;
	}

	public void ResetRegion()
	{
		__pbn__Region = null;
	}
}
