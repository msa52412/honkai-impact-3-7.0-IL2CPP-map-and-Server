using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetRegionUidRangeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1381
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private string __pbn__LocalRegionName;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "local_region_name")]
	[DefaultValue("")]
	public string LocalRegionName
	{
		get
		{
			return __pbn__LocalRegionName ?? "";
		}
		set
		{
			__pbn__LocalRegionName = value;
		}
	}

	[ProtoMember(3, Name = "region_uid_range_list")]
	public List<RegionUidRange> RegionUidRangeLists { get; } = new List<RegionUidRange>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLocalRegionName()
	{
		return __pbn__LocalRegionName != null;
	}

	public void ResetLocalRegionName()
	{
		__pbn__LocalRegionName = null;
	}
}
