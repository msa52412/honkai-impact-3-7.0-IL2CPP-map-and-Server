using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GreedyEndlessEnrollRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3454
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ALREADY_ENROLL")]
		AlreadyEnroll
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NewGroupId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "new_group_id")]
	public uint NewGroupId
	{
		get
		{
			return __pbn__NewGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNewGroupId()
	{
		return __pbn__NewGroupId.HasValue;
	}

	public void ResetNewGroupId()
	{
		__pbn__NewGroupId = null;
	}
}
