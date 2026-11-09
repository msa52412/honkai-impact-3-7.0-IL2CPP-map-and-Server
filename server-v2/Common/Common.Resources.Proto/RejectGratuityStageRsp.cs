using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RejectGratuityStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4119
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageUniqueId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_unique_id")]
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
