using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOfflineResourceBoxReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4213
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ResourceType;

	private uint? __pbn__BoxId;

	private uint? __pbn__BoxNum;

	[ProtoMember(1, Name = "resource_type")]
	public uint ResourceType
	{
		get
		{
			return __pbn__ResourceType.GetValueOrDefault();
		}
		set
		{
			__pbn__ResourceType = value;
		}
	}

	[ProtoMember(2, Name = "box_id")]
	public uint BoxId
	{
		get
		{
			return __pbn__BoxId.GetValueOrDefault();
		}
		set
		{
			__pbn__BoxId = value;
		}
	}

	[ProtoMember(3, Name = "box_num")]
	public uint BoxNum
	{
		get
		{
			return __pbn__BoxNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BoxNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeResourceType()
	{
		return __pbn__ResourceType.HasValue;
	}

	public void ResetResourceType()
	{
		__pbn__ResourceType = null;
	}

	public bool ShouldSerializeBoxId()
	{
		return __pbn__BoxId.HasValue;
	}

	public void ResetBoxId()
	{
		__pbn__BoxId = null;
	}

	public bool ShouldSerializeBoxNum()
	{
		return __pbn__BoxNum.HasValue;
	}

	public void ResetBoxNum()
	{
		__pbn__BoxNum = null;
	}
}
