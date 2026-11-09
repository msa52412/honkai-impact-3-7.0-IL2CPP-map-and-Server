using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessMonsterGroup : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__Progress;

	private uint? __pbn__OldProgress;

	[ProtoMember(1, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
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

	[ProtoMember(3, Name = "old_progress")]
	public uint OldProgress
	{
		get
		{
			return __pbn__OldProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__OldProgress = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeOldProgress()
	{
		return __pbn__OldProgress.HasValue;
	}

	public void ResetOldProgress()
	{
		__pbn__OldProgress = null;
	}
}
