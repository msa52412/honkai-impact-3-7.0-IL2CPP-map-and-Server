using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RegionUidRange : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__RegionName;

	private uint? __pbn__StartUid;

	private uint? __pbn__EndUid;

	[ProtoMember(1, Name = "region_name")]
	[DefaultValue("")]
	public string RegionName
	{
		get
		{
			return __pbn__RegionName ?? "";
		}
		set
		{
			__pbn__RegionName = value;
		}
	}

	[ProtoMember(2, Name = "start_uid")]
	public uint StartUid
	{
		get
		{
			return __pbn__StartUid.GetValueOrDefault();
		}
		set
		{
			__pbn__StartUid = value;
		}
	}

	[ProtoMember(3, Name = "end_uid")]
	public uint EndUid
	{
		get
		{
			return __pbn__EndUid.GetValueOrDefault();
		}
		set
		{
			__pbn__EndUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
	}

	public bool ShouldSerializeStartUid()
	{
		return __pbn__StartUid.HasValue;
	}

	public void ResetStartUid()
	{
		__pbn__StartUid = null;
	}

	public bool ShouldSerializeEndUid()
	{
		return __pbn__EndUid.HasValue;
	}

	public void ResetEndUid()
	{
		__pbn__EndUid = null;
	}
}
