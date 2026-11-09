using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SupportActivitySupportRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4335
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "IN_CD_TIME")]
		InCdTime,
		[ProtoEnum(Name = "HAS_SUPPORT")]
		HasSupport
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SupportIndex;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "support_index")]
	public uint SupportIndex
	{
		get
		{
			return __pbn__SupportIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportIndex = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSupportIndex()
	{
		return __pbn__SupportIndex.HasValue;
	}

	public void ResetSupportIndex()
	{
		__pbn__SupportIndex = null;
	}
}
