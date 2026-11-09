using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncGratuityStageReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4112
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageUniqueId;

	[ProtoMember(1, Name = "stage_unique_id")]
	public uint StageUniqueId
	{
		get
		{
			return __pbn__StageUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageUniqueId = value;
		}
	}

	[ProtoMember(2, Name = "message_list")]
	public List<GratuityStageMessage> MessageLists { get; } = new List<GratuityStageMessage>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageUniqueId()
	{
		return __pbn__StageUniqueId.HasValue;
	}

	public void ResetStageUniqueId()
	{
		__pbn__StageUniqueId = null;
	}
}
