using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatForbidNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1154
	}

	[ProtoContract]
	public enum ForbidType
	{
		[ProtoEnum(Name = "FORBID_COMMON")]
		ForbidCommon,
		[ProtoEnum(Name = "REPEAT_CHAT")]
		RepeatChat
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ForbidFinishTime;

	private ForbidType? __pbn__forbid_type;

	[ProtoMember(1, Name = "forbid_finish_time")]
	public uint ForbidFinishTime
	{
		get
		{
			return __pbn__ForbidFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ForbidFinishTime = value;
		}
	}

	[ProtoMember(2)]
	[DefaultValue(ForbidType.ForbidCommon)]
	public ForbidType forbid_type
	{
		get
		{
			return __pbn__forbid_type.GetValueOrDefault();
		}
		set
		{
			__pbn__forbid_type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeForbidFinishTime()
	{
		return __pbn__ForbidFinishTime.HasValue;
	}

	public void ResetForbidFinishTime()
	{
		__pbn__ForbidFinishTime = null;
	}

	public bool ShouldSerializeforbid_type()
	{
		return __pbn__forbid_type.HasValue;
	}

	public void Resetforbid_type()
	{
		__pbn__forbid_type = null;
	}
}
