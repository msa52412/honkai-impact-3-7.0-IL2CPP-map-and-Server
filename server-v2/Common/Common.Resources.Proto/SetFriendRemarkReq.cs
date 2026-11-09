using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetFriendRemarkReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 972
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__NewRemark;

	private string __pbn__OldRemark;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "new_remark")]
	[DefaultValue("")]
	public string NewRemark
	{
		get
		{
			return __pbn__NewRemark ?? "";
		}
		set
		{
			__pbn__NewRemark = value;
		}
	}

	[ProtoMember(3, Name = "old_remark")]
	[DefaultValue("")]
	public string OldRemark
	{
		get
		{
			return __pbn__OldRemark ?? "";
		}
		set
		{
			__pbn__OldRemark = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeNewRemark()
	{
		return __pbn__NewRemark != null;
	}

	public void ResetNewRemark()
	{
		__pbn__NewRemark = null;
	}

	public bool ShouldSerializeOldRemark()
	{
		return __pbn__OldRemark != null;
	}

	public void ResetOldRemark()
	{
		__pbn__OldRemark = null;
	}
}
