using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EvaluateExaminationReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4329
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ExaminationId;

	private bool? __pbn__IsLike;

	[ProtoMember(1, Name = "examination_id")]
	public uint ExaminationId
	{
		get
		{
			return __pbn__ExaminationId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExaminationId = value;
		}
	}

	[ProtoMember(2, Name = "is_like")]
	public bool IsLike
	{
		get
		{
			return __pbn__IsLike.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLike = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExaminationId()
	{
		return __pbn__ExaminationId.HasValue;
	}

	public void ResetExaminationId()
	{
		__pbn__ExaminationId = null;
	}

	public bool ShouldSerializeIsLike()
	{
		return __pbn__IsLike.HasValue;
	}

	public void ResetIsLike()
	{
		__pbn__IsLike = null;
	}
}
