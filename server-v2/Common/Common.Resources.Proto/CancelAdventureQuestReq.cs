using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CancelAdventureQuestReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3922
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	[ProtoMember(1, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}
}
