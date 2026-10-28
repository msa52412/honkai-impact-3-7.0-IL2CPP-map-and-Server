using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaBossGetMpStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 554
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsFightBody;

	[ProtoMember(1, Name = "uid_list")]
	public uint[] UidLists { get; set; }

	[ProtoMember(2, Name = "is_fight_body")]
	public bool IsFightBody
	{
		get
		{
			return __pbn__IsFightBody.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFightBody = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsFightBody()
	{
		return __pbn__IsFightBody.HasValue;
	}

	public void ResetIsFightBody()
	{
		__pbn__IsFightBody = null;
	}
}
