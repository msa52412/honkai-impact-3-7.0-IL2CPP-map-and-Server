using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBulletinReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 137
	}

	[ProtoContract]
	public enum ReqBulletinType
	{
		[ProtoEnum(Name = "BULLETIN_ALL")]
		BulletinAll,
		[ProtoEnum(Name = "BULLETIN_UPDATE_TIME")]
		BulletinUpdateTime,
		[ProtoEnum(Name = "BULLETIN_TITLE")]
		BulletinTitle,
		[ProtoEnum(Name = "BULLETIN_CONTENT")]
		BulletinContent
	}

	private IExtension __pbn__extensionData;

	private ReqBulletinType? __pbn__Type;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ReqBulletinType.BulletinAll)]
	public ReqBulletinType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "bulletin_id_list")]
	public uint[] BulletinIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
