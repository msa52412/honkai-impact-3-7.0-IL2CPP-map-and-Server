using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetCollaborationScheduleRsp : IExtensible
{
	[ProtoContract]
	public class Schedule : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Id;

		private uint? __pbn__BeginTime;

		private uint? __pbn__EndTime;

		[ProtoMember(1, Name = "id")]
		public uint Id
		{
			get
			{
				return __pbn__Id.GetValueOrDefault();
			}
			set
			{
				__pbn__Id = value;
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

		public bool ShouldSerializeId()
		{
			return __pbn__Id.HasValue;
		}

		public void ResetId()
		{
			__pbn__Id = null;
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

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5832
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

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "schedule_list")]
	public List<Schedule> ScheduleLists { get; } = new List<Schedule>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
