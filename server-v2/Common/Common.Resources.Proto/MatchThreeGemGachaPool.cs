using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeGemGachaPool : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GachaPoolId;

	private MatchThreeGemGachaPoolStatus? __pbn__Status;

	[ProtoMember(1, Name = "gacha_pool_id")]
	public uint GachaPoolId
	{
		get
		{
			return __pbn__GachaPoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaPoolId = value;
		}
	}

	[ProtoMember(2, Name = "status")]
	[DefaultValue(MatchThreeGemGachaPoolStatus.MatchThreeGemGachaPoolLocked)]
	public MatchThreeGemGachaPoolStatus Status
	{
		get
		{
			return __pbn__Status ?? MatchThreeGemGachaPoolStatus.MatchThreeGemGachaPoolLocked;
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(3, Name = "has_got_gem_list")]
	public uint[] HasGotGemLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaPoolId()
	{
		return __pbn__GachaPoolId.HasValue;
	}

	public void ResetGachaPoolId()
	{
		__pbn__GachaPoolId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}
}
