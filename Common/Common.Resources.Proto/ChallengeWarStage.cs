using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChallengeWarStage : IExtensible
{
	[ProtoContract]
	public class StageFloorScore : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Floor;

		private uint? __pbn__Score;

		[ProtoMember(1, Name = "floor")]
		public uint Floor
		{
			get
			{
				return __pbn__Floor.GetValueOrDefault();
			}
			set
			{
				__pbn__Floor = value;
			}
		}

		[ProtoMember(2, Name = "score")]
		public uint Score
		{
			get
			{
				return __pbn__Score.GetValueOrDefault();
			}
			set
			{
				__pbn__Score = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeFloor()
		{
			return __pbn__Floor.HasValue;
		}

		public void ResetFloor()
		{
			__pbn__Floor = null;
		}

		public bool ShouldSerializeScore()
		{
			return __pbn__Score.HasValue;
		}

		public void ResetScore()
		{
			__pbn__Score = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__MaxFinishFloor;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "floor_score_list")]
	public List<StageFloorScore> FloorScoreLists { get; } = new List<StageFloorScore>();


	[ProtoMember(3, Name = "max_finish_floor")]
	public uint MaxFinishFloor
	{
		get
		{
			return __pbn__MaxFinishFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFinishFloor = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeMaxFinishFloor()
	{
		return __pbn__MaxFinishFloor.HasValue;
	}

	public void ResetMaxFinishFloor()
	{
		__pbn__MaxFinishFloor = null;
	}
}
