using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilEventData : IExtensible
{
	[ProtoContract]
	public class CompanyMission : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__MissionId;

		private uint? __pbn__Time;

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

		[ProtoMember(2, Name = "time")]
		public uint Time
		{
			get
			{
				return __pbn__Time.GetValueOrDefault();
			}
			set
			{
				__pbn__Time = value;
			}
		}

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

		public bool ShouldSerializeTime()
		{
			return __pbn__Time.HasValue;
		}

		public void ResetTime()
		{
			__pbn__Time = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(3, Name = "mission")]
	public MasterPupilMission Mission { get; set; }

	[ProtoMember(4, Name = "apply")]
	public MasterPupilApplyMsg Apply { get; set; }

	[ProtoMember(5, Name = "deal")]
	public MasterPupilDealMsg Deal { get; set; }

	[ProtoMember(6, Name = "eval")]
	public MasterPupilEvalMsg Eval { get; set; }

	[ProtoMember(7, Name = "company_mission_list")]
	public List<CompanyMission> CompanyMissionLists { get; } = new List<CompanyMission>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}
}
