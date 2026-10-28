using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeGemGachaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7608
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_POOL")]
		NoPool,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "ACTION_RANDOM_ERROR")]
		ActionRandomError,
		[ProtoEnum(Name = "GEM_ERROR")]
		GemError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__NewGachaRandom;

	private uint? __pbn__PoolId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "new_gacha_random")]
	public uint NewGachaRandom
	{
		get
		{
			return __pbn__NewGachaRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__NewGachaRandom = value;
		}
	}

	[ProtoMember(4, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	[ProtoMember(5, Name = "gem")]
	public MatchThreeGem Gem { get; set; }

	[ProtoMember(6, Name = "pool_list")]
	public List<MatchThreeGemGachaPool> PoolLists { get; } = new List<MatchThreeGemGachaPool>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeNewGachaRandom()
	{
		return __pbn__NewGachaRandom.HasValue;
	}

	public void ResetNewGachaRandom()
	{
		__pbn__NewGachaRandom = null;
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}
}
