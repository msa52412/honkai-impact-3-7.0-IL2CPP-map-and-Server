using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BulletinMission : IExtensible
{
	[ProtoContract]
	public class BulletinMissionCycle : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__CycleId;

		private uint? __pbn__BeginTime;

		private uint? __pbn__EndTime;

		[ProtoMember(1, Name = "cycle_id")]
		public uint CycleId
		{
			get
			{
				return __pbn__CycleId.GetValueOrDefault();
			}
			set
			{
				__pbn__CycleId = value;
			}
		}

		[ProtoMember(2, Name = "begin_time")]
		public uint BeginTime
		{
			get
			{
				return __pbn__BeginTime.GetValueOrDefault();
			}
			set
			{
				__pbn__BeginTime = value;
			}
		}

		[ProtoMember(3, Name = "end_time")]
		public uint EndTime
		{
			get
			{
				return __pbn__EndTime.GetValueOrDefault();
			}
			set
			{
				__pbn__EndTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeCycleId()
		{
			return __pbn__CycleId.HasValue;
		}

		public void ResetCycleId()
		{
			__pbn__CycleId = null;
		}

		public bool ShouldSerializeBeginTime()
		{
			return __pbn__BeginTime.HasValue;
		}

		public void ResetBeginTime()
		{
			__pbn__BeginTime = null;
		}

		public bool ShouldSerializeEndTime()
		{
			return __pbn__EndTime.HasValue;
		}

		public void ResetEndTime()
		{
			__pbn__EndTime = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionId;

	[ProtoMember(1, Name = "mission_id")]
	public uint MissionId
	{
		get
		{
			return __pbn__MissionId.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionId = value;
		}
	}

	[ProtoMember(2, Name = "cycle_list")]
	public List<BulletinMissionCycle> CycleLists { get; } = new List<BulletinMissionCycle>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMissionId()
	{
		return __pbn__MissionId.HasValue;
	}

	public void ResetMissionId()
	{
		__pbn__MissionId = null;
	}
}
