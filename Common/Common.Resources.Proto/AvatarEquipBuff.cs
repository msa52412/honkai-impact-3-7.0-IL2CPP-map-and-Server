using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarEquipBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private string __pbn__BuffName;

	private uint? __pbn__BuffNum;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "buff_name")]
	[DefaultValue("")]
	public string BuffName
	{
		get
		{
			return __pbn__BuffName ?? "";
		}
		set
		{
			__pbn__BuffName = value;
		}
	}

	[ProtoMember(3, Name = "buff_num")]
	public uint BuffNum
	{
		get
		{
			return __pbn__BuffNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeBuffName()
	{
		return __pbn__BuffName != null;
	}

	public void ResetBuffName()
	{
		__pbn__BuffName = null;
	}

	public bool ShouldSerializeBuffNum()
	{
		return __pbn__BuffNum.HasValue;
	}

	public void ResetBuffNum()
	{
		__pbn__BuffNum = null;
	}
}
