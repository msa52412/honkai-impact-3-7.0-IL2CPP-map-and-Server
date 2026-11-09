using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterActiveTalentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4664
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "TALENT_ERROR")]
		TalentError,
		[ProtoEnum(Name = "BUILDING_LOCKED")]
		BuildingLocked,
		[ProtoEnum(Name = "UNIQUE_TAG_REPEATED")]
		UniqueTagRepeated
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
