using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClientMailInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TotalNum;

	private uint? __pbn__CanFastDeleteNum;

	[ProtoMember(1, Name = "total_num")]
	public uint TotalNum
	{
		get
		{
			return __pbn__TotalNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalNum = value;
		}
	}

	[ProtoMember(2, Name = "can_fast_delete_num")]
	public uint CanFastDeleteNum
	{
		get
		{
			return __pbn__CanFastDeleteNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CanFastDeleteNum = value;
		}
	}

	[ProtoMember(3, Name = "mail_style_info_list")]
	public List<ClientMailStyleInfo> MailStyleInfoLists { get; } = new List<ClientMailStyleInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTotalNum()
	{
		return __pbn__TotalNum.HasValue;
	}

	public void ResetTotalNum()
	{
		__pbn__TotalNum = null;
	}

	public bool ShouldSerializeCanFastDeleteNum()
	{
		return __pbn__CanFastDeleteNum.HasValue;
	}

	public void ResetCanFastDeleteNum()
	{
		__pbn__CanFastDeleteNum = null;
	}
}
