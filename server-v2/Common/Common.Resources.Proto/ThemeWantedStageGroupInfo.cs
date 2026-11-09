using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ThemeWantedStageGroupInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageGroupId;

	private uint? __pbn__Progress;

	[ProtoMember(1, Name = "stage_group_id")]
	public uint StageGroupId
	{
		get
		{
			return __pbn__StageGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageGroupId = value;
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

	[ProtoMember(3, Name = "not_pass_progress_list")]
	public uint[] NotPassProgressLists { get; set; }

	[ProtoMember(4, Name = "unlock_mp_progress_list")]
	public uint[] UnlockMpProgressLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageGroupId()
	{
		return __pbn__StageGroupId.HasValue;
	}

	public void ResetStageGroupId()
	{
		__pbn__StageGroupId = null;
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
