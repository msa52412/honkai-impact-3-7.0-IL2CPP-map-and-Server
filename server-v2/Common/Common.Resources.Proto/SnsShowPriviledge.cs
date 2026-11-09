using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SnsShowPriviledge : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SnsShowType;

	private uint? __pbn__SnsShowPriviledgeState;

	[ProtoMember(1, Name = "sns_show_type")]
	public uint SnsShowType
	{
		get
		{
			return __pbn__SnsShowType.GetValueOrDefault();
		}
		set
		{
			__pbn__SnsShowType = value;
		}
	}

	[ProtoMember(2, Name = "sns_show_priviledge_state")]
	public uint SnsShowPriviledgeState
	{
		get
		{
			return __pbn__SnsShowPriviledgeState.GetValueOrDefault();
		}
		set
		{
			__pbn__SnsShowPriviledgeState = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSnsShowType()
	{
		return __pbn__SnsShowType.HasValue;
	}

	public void ResetSnsShowType()
	{
		__pbn__SnsShowType = null;
	}

	public bool ShouldSerializeSnsShowPriviledgeState()
	{
		return __pbn__SnsShowPriviledgeState.HasValue;
	}

	public void ResetSnsShowPriviledgeState()
	{
		__pbn__SnsShowPriviledgeState = null;
	}
}
