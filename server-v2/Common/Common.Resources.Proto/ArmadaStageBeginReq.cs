using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaStageBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2669
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsFightBody;

	private bool? __pbn__IsMpMode;

	[ProtoMember(1, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

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

	[ProtoMember(3, Name = "is_mp_mode")]
	public bool IsMpMode
	{
		get
		{
			return __pbn__IsMpMode.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMpMode = value;
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

	public bool ShouldSerializeIsMpMode()
	{
		return __pbn__IsMpMode.HasValue;
	}

	public void ResetIsMpMode()
	{
		__pbn__IsMpMode = null;
	}
}
