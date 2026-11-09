using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleQAvatarBattle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScore;

	private uint? __pbn__HasTakeRewardScore;

	private uint? __pbn__ChooseVirtualAvatarId;

	private uint? __pbn__WinNum;

	private uint? __pbn__WinStreak;

	private uint? __pbn__VirtualAvatarDropNum;

	[ProtoMember(1, Name = "cur_score")]
	public uint CurScore
	{
		get
		{
			return __pbn__CurScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScore = value;
		}
	}

	[ProtoMember(2, Name = "has_take_reward_score")]
	public uint HasTakeRewardScore
	{
		get
		{
			return __pbn__HasTakeRewardScore.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardScore = value;
		}
	}

	[ProtoMember(3, Name = "choose_virtual_avatar_id")]
	public uint ChooseVirtualAvatarId
	{
		get
		{
			return __pbn__ChooseVirtualAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChooseVirtualAvatarId = value;
		}
	}

	[ProtoMember(4, Name = "win_num")]
	public uint WinNum
	{
		get
		{
			return __pbn__WinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__WinNum = value;
		}
	}

	[ProtoMember(5, Name = "win_streak")]
	public uint WinStreak
	{
		get
		{
			return __pbn__WinStreak.GetValueOrDefault();
		}
		set
		{
			__pbn__WinStreak = value;
		}
	}

	[ProtoMember(6, Name = "virtual_avatar_drop_num")]
	public uint VirtualAvatarDropNum
	{
		get
		{
			return __pbn__VirtualAvatarDropNum.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualAvatarDropNum = value;
		}
	}

	[ProtoMember(7, Name = "got_material_list")]
	public List<RpgTaleQAvatarSettleGotMaterial> GotMaterialLists { get; } = new List<RpgTaleQAvatarSettleGotMaterial>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurScore()
	{
		return __pbn__CurScore.HasValue;
	}

	public void ResetCurScore()
	{
		__pbn__CurScore = null;
	}

	public bool ShouldSerializeHasTakeRewardScore()
	{
		return __pbn__HasTakeRewardScore.HasValue;
	}

	public void ResetHasTakeRewardScore()
	{
		__pbn__HasTakeRewardScore = null;
	}

	public bool ShouldSerializeChooseVirtualAvatarId()
	{
		return __pbn__ChooseVirtualAvatarId.HasValue;
	}

	public void ResetChooseVirtualAvatarId()
	{
		__pbn__ChooseVirtualAvatarId = null;
	}

	public bool ShouldSerializeWinNum()
	{
		return __pbn__WinNum.HasValue;
	}

	public void ResetWinNum()
	{
		__pbn__WinNum = null;
	}

	public bool ShouldSerializeWinStreak()
	{
		return __pbn__WinStreak.HasValue;
	}

	public void ResetWinStreak()
	{
		__pbn__WinStreak = null;
	}

	public bool ShouldSerializeVirtualAvatarDropNum()
	{
		return __pbn__VirtualAvatarDropNum.HasValue;
	}

	public void ResetVirtualAvatarDropNum()
	{
		__pbn__VirtualAvatarDropNum = null;
	}
}
