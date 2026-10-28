using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarArtifactSwitch : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private bool? __pbn__IsArtifactSwitchOn;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "is_artifact_switch_on")]
	public bool IsArtifactSwitchOn
	{
		get
		{
			return __pbn__IsArtifactSwitchOn.GetValueOrDefault();
		}
		set
		{
			__pbn__IsArtifactSwitchOn = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeIsArtifactSwitchOn()
	{
		return __pbn__IsArtifactSwitchOn.HasValue;
	}

	public void ResetIsArtifactSwitchOn()
	{
		__pbn__IsArtifactSwitchOn = null;
	}
}
