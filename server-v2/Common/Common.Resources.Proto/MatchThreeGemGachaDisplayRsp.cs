using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeGemGachaDisplayRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7606
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

	private uint? __pbn__ActivityId;

	private uint? __pbn__GachaRandom;

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

	[ProtoMember(3, Name = "gacha_random")]
	public uint GachaRandom
	{
		get
		{
			return __pbn__GachaRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaRandom = value;
		}
	}

	[ProtoMember(4, Name = "pool_list")]
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

	public bool ShouldSerializeGachaRandom()
	{
		return __pbn__GachaRandom.HasValue;
	}

	public void ResetGachaRandom()
	{
		__pbn__GachaRandom = null;
	}
}
