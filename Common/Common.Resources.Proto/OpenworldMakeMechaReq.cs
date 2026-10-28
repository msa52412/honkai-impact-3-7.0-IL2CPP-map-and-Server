using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldMakeMechaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4516
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PaperId;

	[ProtoMember(1, Name = "paper_id")]
	public uint PaperId
	{
		get
		{
			return __pbn__PaperId.GetValueOrDefault();
		}
		set
		{
			__pbn__PaperId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePaperId()
	{
		return __pbn__PaperId.HasValue;
	}

	public void ResetPaperId()
	{
		__pbn__PaperId = null;
	}
}
