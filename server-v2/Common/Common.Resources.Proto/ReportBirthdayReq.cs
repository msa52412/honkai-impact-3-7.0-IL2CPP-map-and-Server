using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportBirthdayReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 299
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Birthday;

	[ProtoMember(1, Name = "birthday")]
	public uint Birthday
	{
		get
		{
			return __pbn__Birthday.GetValueOrDefault();
		}
		set
		{
			__pbn__Birthday = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBirthday()
	{
		return __pbn__Birthday.HasValue;
	}

	public void ResetBirthday()
	{
		__pbn__Birthday = null;
	}
}
