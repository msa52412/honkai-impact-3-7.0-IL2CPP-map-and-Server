using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgGetBriefRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6072
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_BATTLE")]
		NotInBattle
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AllCountryRank;

	private uint? __pbn__SelfCountryRank;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "all_country_rank")]
	public uint AllCountryRank
	{
		get
		{
			return __pbn__AllCountryRank.GetValueOrDefault();
		}
		set
		{
			__pbn__AllCountryRank = value;
		}
	}

	[ProtoMember(3, Name = "self_country_rank")]
	public uint SelfCountryRank
	{
		get
		{
			return __pbn__SelfCountryRank.GetValueOrDefault();
		}
		set
		{
			__pbn__SelfCountryRank = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAllCountryRank()
	{
		return __pbn__AllCountryRank.HasValue;
	}

	public void ResetAllCountryRank()
	{
		__pbn__AllCountryRank = null;
	}

	public bool ShouldSerializeSelfCountryRank()
	{
		return __pbn__SelfCountryRank.HasValue;
	}

	public void ResetSelfCountryRank()
	{
		__pbn__SelfCountryRank = null;
	}
}
