using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarTeam : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageType;

	[ProtoMember(1, Name = "stage_type")]
	public uint StageType
	{
		get
		{
			return __pbn__StageType.GetValueOrDefault();
		}
		set
		{
			__pbn__StageType = value;
		}
	}

	[ProtoMember(2, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageType()
	{
		return __pbn__StageType.HasValue;
	}

	public void ResetStageType()
	{
		__pbn__StageType = null;
	}
}
