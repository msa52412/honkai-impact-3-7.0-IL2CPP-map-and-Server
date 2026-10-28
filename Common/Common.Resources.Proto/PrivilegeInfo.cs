using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PrivilegeInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PrivilegeId;

	private uint? __pbn__ExpireTime;

	[ProtoMember(1, Name = "privilege_id")]
	public uint PrivilegeId
	{
		get
		{
			return __pbn__PrivilegeId.GetValueOrDefault();
		}
		set
		{
			__pbn__PrivilegeId = value;
		}
	}

	[ProtoMember(2, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	[ProtoMember(3, Name = "right_info_list")]
	public List<PrivilegeRightInfo> RightInfoLists { get; } = new List<PrivilegeRightInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePrivilegeId()
	{
		return __pbn__PrivilegeId.HasValue;
	}

	public void ResetPrivilegeId()
	{
		__pbn__PrivilegeId = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}
}
