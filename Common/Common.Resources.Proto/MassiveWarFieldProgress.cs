using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarFieldProgress : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FieldProgressRatio;

	private uint? __pbn__InFieldMemberNum;

	[ProtoMember(1, Name = "field_progress_ratio")]
	public uint FieldProgressRatio
	{
		get
		{
			return __pbn__FieldProgressRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__FieldProgressRatio = value;
		}
	}

	[ProtoMember(2, Name = "in_field_member_num")]
	public uint InFieldMemberNum
	{
		get
		{
			return __pbn__InFieldMemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__InFieldMemberNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFieldProgressRatio()
	{
		return __pbn__FieldProgressRatio.HasValue;
	}

	public void ResetFieldProgressRatio()
	{
		__pbn__FieldProgressRatio = null;
	}

	public bool ShouldSerializeInFieldMemberNum()
	{
		return __pbn__InFieldMemberNum.HasValue;
	}

	public void ResetInFieldMemberNum()
	{
		__pbn__InFieldMemberNum = null;
	}
}
