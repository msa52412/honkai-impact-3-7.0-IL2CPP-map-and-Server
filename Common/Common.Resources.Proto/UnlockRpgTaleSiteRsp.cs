using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockRpgTaleSiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2308
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "UNLOCKED")]
		Unlocked,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough,
		[ProtoEnum(Name = "FLAG_LIMIT")]
		FlagLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__TaleId;

	private bool? __pbn__IsOnlyCheck;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(5, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(6, Name = "limit_flag_list")]
	public List<RpgTaleFlag> LimitFlagLists { get; } = new List<RpgTaleFlag>();


	[ProtoMember(7, Name = "is_only_check")]
	public bool IsOnlyCheck
	{
		get
		{
			return __pbn__IsOnlyCheck.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOnlyCheck = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeIsOnlyCheck()
	{
		return __pbn__IsOnlyCheck.HasValue;
	}

	public void ResetIsOnlyCheck()
	{
		__pbn__IsOnlyCheck = null;
	}
}
