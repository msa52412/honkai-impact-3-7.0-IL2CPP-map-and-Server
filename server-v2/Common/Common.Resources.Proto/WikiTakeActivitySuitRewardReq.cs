using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WikiTakeActivitySuitRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1592
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SuitId;

	[ProtoMember(1, Name = "suit_id")]
	public uint SuitId
	{
		get
		{
			return __pbn__SuitId.GetValueOrDefault();
		}
		set
		{
			__pbn__SuitId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSuitId()
	{
		return __pbn__SuitId.HasValue;
	}

	public void ResetSuitId()
	{
		__pbn__SuitId = null;
	}
}
