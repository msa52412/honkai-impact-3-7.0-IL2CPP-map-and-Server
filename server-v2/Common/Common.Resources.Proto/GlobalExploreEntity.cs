using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreEntity : IExtensible
{
	[ProtoContract]
	public class KingdomData : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__KingdomId;

		private uint? __pbn__Progress;

		[ProtoMember(1, Name = "kingdom_id")]
		public uint KingdomId
		{
			get
			{
				return __pbn__KingdomId.GetValueOrDefault();
			}
			set
			{
				__pbn__KingdomId = value;
			}
		}

		[ProtoMember(2, Name = "progress")]
		public uint Progress
		{
			get
			{
				return __pbn__Progress.GetValueOrDefault();
			}
			set
			{
				__pbn__Progress = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeKingdomId()
		{
			return __pbn__KingdomId.HasValue;
		}

		public void ResetKingdomId()
		{
			__pbn__KingdomId = null;
		}

		public bool ShouldSerializeProgress()
		{
			return __pbn__Progress.HasValue;
		}

		public void ResetProgress()
		{
			__pbn__Progress = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private bool? __pbn__IsActive;

	private uint? __pbn__ActiveTime;

	private uint? __pbn__Contribution;

	[ProtoMember(1, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	[ProtoMember(2, Name = "is_active")]
	public bool IsActive
	{
		get
		{
			return __pbn__IsActive.GetValueOrDefault();
		}
		set
		{
			__pbn__IsActive = value;
		}
	}

	[ProtoMember(3, Name = "kingdom_data_list")]
	public List<KingdomData> KingdomDataLists { get; } = new List<KingdomData>();


	[ProtoMember(4, Name = "active_time")]
	public uint ActiveTime
	{
		get
		{
			return __pbn__ActiveTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveTime = value;
		}
	}

	[ProtoMember(5, Name = "contribution")]
	public uint Contribution
	{
		get
		{
			return __pbn__Contribution.GetValueOrDefault();
		}
		set
		{
			__pbn__Contribution = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}

	public bool ShouldSerializeIsActive()
	{
		return __pbn__IsActive.HasValue;
	}

	public void ResetIsActive()
	{
		__pbn__IsActive = null;
	}

	public bool ShouldSerializeActiveTime()
	{
		return __pbn__ActiveTime.HasValue;
	}

	public void ResetActiveTime()
	{
		__pbn__ActiveTime = null;
	}

	public bool ShouldSerializeContribution()
	{
		return __pbn__Contribution.HasValue;
	}

	public void ResetContribution()
	{
		__pbn__Contribution = null;
	}
}
