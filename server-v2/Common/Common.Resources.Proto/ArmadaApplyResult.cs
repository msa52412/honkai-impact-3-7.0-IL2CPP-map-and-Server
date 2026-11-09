using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaApplyResult : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private bool? __pbn__Agree;

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

	[ProtoMember(3, Name = "agree")]
	public bool Agree
	{
		get
		{
			return __pbn__Agree.GetValueOrDefault();
		}
		set
		{
			__pbn__Agree = value;
		}
	}

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

	public bool ShouldSerializeAgree()
	{
		return __pbn__Agree.HasValue;
	}

	public void ResetAgree()
	{
		__pbn__Agree = null;
	}
}
