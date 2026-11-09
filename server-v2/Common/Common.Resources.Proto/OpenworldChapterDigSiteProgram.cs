using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterDigSiteProgramTimes : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ProgramId;

	private uint? __pbn__Times;

	[ProtoMember(1, Name = "program_id")]
	public uint ProgramId
	{
		get
		{
			return __pbn__ProgramId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgramId = value;
		}
	}

	[ProtoMember(2, Name = "times")]
	public uint Times
	{
		get
		{
			return __pbn__Times.GetValueOrDefault();
		}
		set
		{
			__pbn__Times = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeProgramId()
	{
		return __pbn__ProgramId.HasValue;
	}

	public void ResetProgramId()
	{
		__pbn__ProgramId = null;
	}

	public bool ShouldSerializeTimes()
	{
		return __pbn__Times.HasValue;
	}

	public void ResetTimes()
	{
		__pbn__Times = null;
	}
}
