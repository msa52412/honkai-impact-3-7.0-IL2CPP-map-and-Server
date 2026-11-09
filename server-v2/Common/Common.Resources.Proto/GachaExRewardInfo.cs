using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaExRewardInfo : IExtensible
{
	[ProtoContract]
	public class ExReward : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__NeedGachaTimes;

		private uint? __pbn__RewardId;

		private bool? __pbn__IsTaken;

		[ProtoMember(1, Name = "need_gacha_times")]
		public uint NeedGachaTimes
		{
			get
			{
				return __pbn__NeedGachaTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__NeedGachaTimes = value;
			}
		}

		[ProtoMember(2, Name = "reward_id")]
		public uint RewardId
		{
			get
			{
				return __pbn__RewardId.GetValueOrDefault();
			}
			set
			{
				__pbn__RewardId = value;
			}
		}

		[ProtoMember(3, Name = "is_taken")]
		public bool IsTaken
		{
			get
			{
				return __pbn__IsTaken.GetValueOrDefault();
			}
			set
			{
				__pbn__IsTaken = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeNeedGachaTimes()
		{
			return __pbn__NeedGachaTimes.HasValue;
		}

		public void ResetNeedGachaTimes()
		{
			__pbn__NeedGachaTimes = null;
		}

		public bool ShouldSerializeRewardId()
		{
			return __pbn__RewardId.HasValue;
		}

		public void ResetRewardId()
		{
			__pbn__RewardId = null;
		}

		public bool ShouldSerializeIsTaken()
		{
			return __pbn__IsTaken.HasValue;
		}

		public void ResetIsTaken()
		{
			__pbn__IsTaken = null;
		}
	}

	[ProtoContract]
	public class ExRewardDisplayDetail : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__NeedGachaTimes;

		private uint? __pbn__ItemId;

		private uint? __pbn__ItemNum;

		private bool? __pbn__IsTaken;

		private bool? __pbn__IsDisplayShining;

		[ProtoMember(1, Name = "need_gacha_times")]
		public uint NeedGachaTimes
		{
			get
			{
				return __pbn__NeedGachaTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__NeedGachaTimes = value;
			}
		}

		[ProtoMember(2, Name = "item_id")]
		public uint ItemId
		{
			get
			{
				return __pbn__ItemId.GetValueOrDefault();
			}
			set
			{
				__pbn__ItemId = value;
			}
		}

		[ProtoMember(3, Name = "item_num")]
		public uint ItemNum
		{
			get
			{
				return __pbn__ItemNum.GetValueOrDefault();
			}
			set
			{
				__pbn__ItemNum = value;
			}
		}

		[ProtoMember(4, Name = "is_taken")]
		public bool IsTaken
		{
			get
			{
				return __pbn__IsTaken.GetValueOrDefault();
			}
			set
			{
				__pbn__IsTaken = value;
			}
		}

		[ProtoMember(5, Name = "is_display_shining")]
		public bool IsDisplayShining
		{
			get
			{
				return __pbn__IsDisplayShining.GetValueOrDefault();
			}
			set
			{
				__pbn__IsDisplayShining = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeNeedGachaTimes()
		{
			return __pbn__NeedGachaTimes.HasValue;
		}

		public void ResetNeedGachaTimes()
		{
			__pbn__NeedGachaTimes = null;
		}

		public bool ShouldSerializeItemId()
		{
			return __pbn__ItemId.HasValue;
		}

		public void ResetItemId()
		{
			__pbn__ItemId = null;
		}

		public bool ShouldSerializeItemNum()
		{
			return __pbn__ItemNum.HasValue;
		}

		public void ResetItemNum()
		{
			__pbn__ItemNum = null;
		}

		public bool ShouldSerializeIsTaken()
		{
			return __pbn__IsTaken.HasValue;
		}

		public void ResetIsTaken()
		{
			__pbn__IsTaken = null;
		}

		public bool ShouldSerializeIsDisplayShining()
		{
			return __pbn__IsDisplayShining.HasValue;
		}

		public void ResetIsDisplayShining()
		{
			__pbn__IsDisplayShining = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DisplayExRewardItem;

	private uint? __pbn__DisplayExRewardType;

	[ProtoMember(15, Name = "ex_reward_list")]
	public List<ExReward> ExRewardLists { get; } = new List<ExReward>();


	[ProtoMember(16, Name = "display_ex_reward_item")]
	public uint DisplayExRewardItem
	{
		get
		{
			return __pbn__DisplayExRewardItem.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayExRewardItem = value;
		}
	}

	[ProtoMember(17, Name = "display_ex_reward_type")]
	public uint DisplayExRewardType
	{
		get
		{
			return __pbn__DisplayExRewardType.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayExRewardType = value;
		}
	}

	[ProtoMember(18, Name = "display_ex_reward_detail_list")]
	public List<ExRewardDisplayDetail> DisplayExRewardDetailLists { get; } = new List<ExRewardDisplayDetail>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDisplayExRewardItem()
	{
		return __pbn__DisplayExRewardItem.HasValue;
	}

	public void ResetDisplayExRewardItem()
	{
		__pbn__DisplayExRewardItem = null;
	}

	public bool ShouldSerializeDisplayExRewardType()
	{
		return __pbn__DisplayExRewardType.HasValue;
	}

	public void ResetDisplayExRewardType()
	{
		__pbn__DisplayExRewardType = null;
	}
}
