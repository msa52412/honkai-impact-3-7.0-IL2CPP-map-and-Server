using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpSetExtraSkillReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1644
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ExtraSkillId;

	[ProtoMember(1, Name = "extra_skill_id")]
	public uint ExtraSkillId
	{
		get
		{
			return __pbn__ExtraSkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraSkillId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExtraSkillId()
	{
		return __pbn__ExtraSkillId.HasValue;
	}

	public void ResetExtraSkillId()
	{
		__pbn__ExtraSkillId = null;
	}
}
