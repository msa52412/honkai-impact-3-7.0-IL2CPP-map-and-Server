using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualTrainEvoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3522
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__EvoAvatarId;

	private bool? __pbn__IsQuickEvo;

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

	[ProtoMember(2, Name = "evo_avatar_id")]
	public uint EvoAvatarId
	{
		get
		{
			return __pbn__EvoAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__EvoAvatarId = value;
		}
	}

	[ProtoMember(3, Name = "is_quick_evo")]
	public bool IsQuickEvo
	{
		get
		{
			return __pbn__IsQuickEvo.GetValueOrDefault();
		}
		set
		{
			__pbn__IsQuickEvo = value;
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

	public bool ShouldSerializeEvoAvatarId()
	{
		return __pbn__EvoAvatarId.HasValue;
	}

	public void ResetEvoAvatarId()
	{
		__pbn__EvoAvatarId = null;
	}

	public bool ShouldSerializeIsQuickEvo()
	{
		return __pbn__IsQuickEvo.HasValue;
	}

	public void ResetIsQuickEvo()
	{
		__pbn__IsQuickEvo = null;
	}
}
