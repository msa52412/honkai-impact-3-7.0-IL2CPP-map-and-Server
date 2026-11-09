using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarArtifactDetail : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ArtifactId;

	private uint? __pbn__ArtifactLevel;

	private bool? __pbn__IsArtifactSwitchOn;

	[ProtoMember(1, Name = "artifact_id")]
	public uint ArtifactId
	{
		get
		{
			return __pbn__ArtifactId.GetValueOrDefault();
		}
		set
		{
			__pbn__ArtifactId = value;
		}
	}

	[ProtoMember(2, Name = "artifact_level")]
	public uint ArtifactLevel
	{
		get
		{
			return __pbn__ArtifactLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ArtifactLevel = value;
		}
	}

	[ProtoMember(3, Name = "is_artifact_switch_on")]
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

	public bool ShouldSerializeArtifactId()
	{
		return __pbn__ArtifactId.HasValue;
	}

	public void ResetArtifactId()
	{
		__pbn__ArtifactId = null;
	}

	public bool ShouldSerializeArtifactLevel()
	{
		return __pbn__ArtifactLevel.HasValue;
	}

	public void ResetArtifactLevel()
	{
		__pbn__ArtifactLevel = null;
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
