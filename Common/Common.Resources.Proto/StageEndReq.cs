using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 45
	}

	private IExtension __pbn__extensionData;

	private byte[] __pbn__Body;

	private string __pbn__Sign;

	[ProtoMember(1, Name = "body")]
	public byte[] Body
	{
		get
		{
			return __pbn__Body;
		}
		set
		{
			__pbn__Body = value;
		}
	}

	[ProtoMember(2, Name = "sign")]
	[DefaultValue("")]
	public string Sign
	{
		get
		{
			return __pbn__Sign ?? "";
		}
		set
		{
			__pbn__Sign = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBody()
	{
		return __pbn__Body != null;
	}

	public void ResetBody()
	{
		__pbn__Body = null;
	}

	public bool ShouldSerializeSign()
	{
		return __pbn__Sign != null;
	}

	public void ResetSign()
	{
		__pbn__Sign = null;
	}
}
