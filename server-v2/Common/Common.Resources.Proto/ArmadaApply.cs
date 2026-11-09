using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaApply : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "brief_data")]
	public PlayerFriendBriefData BriefData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}
}
