using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockDLCPhotoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3586
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueMonsterId;

	[ProtoMember(1, Name = "unique_monster_id")]
	public uint UniqueMonsterId
	{
		get
		{
			return __pbn__UniqueMonsterId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueMonsterId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueMonsterId()
	{
		return __pbn__UniqueMonsterId.HasValue;
	}

	public void ResetUniqueMonsterId()
	{
		__pbn__UniqueMonsterId = null;
	}
}
