using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarArtifactUnlockReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1444
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__ArtifactId;

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

	[ProtoMember(2, Name = "artifact_id")]
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

	public bool ShouldSerializeArtifactId()
	{
		return __pbn__ArtifactId.HasValue;
	}

	public void ResetArtifactId()
	{
		__pbn__ArtifactId = null;
	}
}
