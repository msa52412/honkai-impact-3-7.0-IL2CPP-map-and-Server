using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SurveyData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private string __pbn__Url;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "url")]
	[DefaultValue("")]
	public string Url
	{
		get
		{
			return __pbn__Url ?? "";
		}
		set
		{
			__pbn__Url = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeUrl()
	{
		return __pbn__Url != null;
	}

	public void ResetUrl()
	{
		__pbn__Url = null;
	}
}
