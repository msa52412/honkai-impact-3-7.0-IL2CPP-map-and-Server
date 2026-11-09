using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JigsawData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__JigsawId;

	private bool? __pbn__IsFinishRewardTaken;

	private bool? __pbn__IsFinished;

	[ProtoMember(1, Name = "jigsaw_id")]
	public uint JigsawId
	{
		get
		{
			return __pbn__JigsawId.GetValueOrDefault();
		}
		set
		{
			__pbn__JigsawId = value;
		}
	}

	[ProtoMember(2, Name = "block_list")]
	public List<JigsawBlockData> BlockLists { get; } = new List<JigsawBlockData>();


	[ProtoMember(3, Name = "waiting_piece_id_list")]
	public uint[] WaitingPieceIdLists { get; set; }

	[ProtoMember(4, Name = "taken_reward_group_id_list")]
	public uint[] TakenRewardGroupIdLists { get; set; }

	[ProtoMember(5, Name = "is_finish_reward_taken")]
	public bool IsFinishRewardTaken
	{
		get
		{
			return __pbn__IsFinishRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinishRewardTaken = value;
		}
	}

	[ProtoMember(6, Name = "is_finished")]
	public bool IsFinished
	{
		get
		{
			return __pbn__IsFinished.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinished = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeJigsawId()
	{
		return __pbn__JigsawId.HasValue;
	}

	public void ResetJigsawId()
	{
		__pbn__JigsawId = null;
	}

	public bool ShouldSerializeIsFinishRewardTaken()
	{
		return __pbn__IsFinishRewardTaken.HasValue;
	}

	public void ResetIsFinishRewardTaken()
	{
		__pbn__IsFinishRewardTaken = null;
	}

	public bool ShouldSerializeIsFinished()
	{
		return __pbn__IsFinished.HasValue;
	}

	public void ResetIsFinished()
	{
		__pbn__IsFinished = null;
	}
}
