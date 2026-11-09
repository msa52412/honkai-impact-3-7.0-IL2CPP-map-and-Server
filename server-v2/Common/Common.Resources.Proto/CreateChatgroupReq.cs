using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateChatgroupReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2200
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private string __pbn__Notice;

	private uint? __pbn__IconId;

	[ProtoMember(1, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(2, Name = "notice")]
	[DefaultValue("")]
	public string Notice
	{
		get
		{
			return __pbn__Notice ?? "";
		}
		set
		{
			__pbn__Notice = value;
		}
	}

	[ProtoMember(3, Name = "icon_id")]
	public uint IconId
	{
		get
		{
			return __pbn__IconId.GetValueOrDefault();
		}
		set
		{
			__pbn__IconId = value;
		}
	}

	[ProtoMember(4, Name = "label_id_list")]
	public uint[] LabelIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeNotice()
	{
		return __pbn__Notice != null;
	}

	public void ResetNotice()
	{
		__pbn__Notice = null;
	}

	public bool ShouldSerializeIconId()
	{
		return __pbn__IconId.HasValue;
	}

	public void ResetIconId()
	{
		__pbn__IconId = null;
	}
}
