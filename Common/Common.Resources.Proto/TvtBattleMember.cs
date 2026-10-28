using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__RegionName;

	private bool? __pbn__IsPrepared;

	private TvtBattleMemberStatus? __pbn__MemberStatus;

	private bool? __pbn__IsConfirmed;

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

	[ProtoMember(2, Name = "region_name")]
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

	[ProtoMember(3, Name = "is_prepared")]
	public bool IsPrepared
	{
		get
		{
			return __pbn__IsPrepared.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPrepared = value;
		}
	}

	[ProtoMember(4, Name = "member_status")]
	[DefaultValue(TvtBattleMemberStatus.TvtBattleMemberStatusNone)]
	public TvtBattleMemberStatus MemberStatus
	{
		get
		{
			return __pbn__MemberStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__MemberStatus = value;
		}
	}

	[ProtoMember(5, Name = "is_confirmed")]
	public bool IsConfirmed
	{
		get
		{
			return __pbn__IsConfirmed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsConfirmed = value;
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

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
	}

	public bool ShouldSerializeIsPrepared()
	{
		return __pbn__IsPrepared.HasValue;
	}

	public void ResetIsPrepared()
	{
		__pbn__IsPrepared = null;
	}

	public bool ShouldSerializeMemberStatus()
	{
		return __pbn__MemberStatus.HasValue;
	}

	public void ResetMemberStatus()
	{
		__pbn__MemberStatus = null;
	}

	public bool ShouldSerializeIsConfirmed()
	{
		return __pbn__IsConfirmed.HasValue;
	}

	public void ResetIsConfirmed()
	{
		__pbn__IsConfirmed = null;
	}
}
