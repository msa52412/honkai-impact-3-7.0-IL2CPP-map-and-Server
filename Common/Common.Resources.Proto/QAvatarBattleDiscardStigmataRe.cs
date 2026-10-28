using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleDiscardStigmataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5612
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StigmataId;

	[ProtoMember(1, Name = "stigmata_id")]
	public uint StigmataId
	{
		get
		{
			return __pbn__StigmataId.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStigmataId()
	{
		return __pbn__StigmataId.HasValue;
	}

	public void ResetStigmataId()
	{
		__pbn__StigmataId = null;
	}
}
