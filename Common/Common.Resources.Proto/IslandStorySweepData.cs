using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class IslandStorySweepData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SweepId;

	private uint? __pbn__OverTime;

	private bool? __pbn__IsFinished;

	private bool? __pbn__IsSieged;

	[ProtoMember(1, Name = "sweep_id")]
	public uint SweepId
	{
		get
		{
			return __pbn__SweepId.GetValueOrDefault();
		}
		set
		{
			__pbn__SweepId = value;
		}
	}

	[ProtoMember(2, Name = "over_time")]
	public uint OverTime
	{
		get
		{
			return __pbn__OverTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OverTime = value;
		}
	}

	[ProtoMember(3, Name = "is_finished")]
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

	[ProtoMember(4, Name = "is_sieged")]
	public bool IsSieged
	{
		get
		{
			return __pbn__IsSieged.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSieged = value;
		}
	}

	[ProtoMember(5, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSweepId()
	{
		return __pbn__SweepId.HasValue;
	}

	public void ResetSweepId()
	{
		__pbn__SweepId = null;
	}

	public bool ShouldSerializeOverTime()
	{
		return __pbn__OverTime.HasValue;
	}

	public void ResetOverTime()
	{
		__pbn__OverTime = null;
	}

	public bool ShouldSerializeIsFinished()
	{
		return __pbn__IsFinished.HasValue;
	}

	public void ResetIsFinished()
	{
		__pbn__IsFinished = null;
	}

	public bool ShouldSerializeIsSieged()
	{
		return __pbn__IsSieged.HasValue;
	}

	public void ResetIsSieged()
	{
		__pbn__IsSieged = null;
	}
}
