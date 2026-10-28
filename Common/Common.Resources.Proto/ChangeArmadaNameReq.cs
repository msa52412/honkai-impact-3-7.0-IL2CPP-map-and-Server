using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChangeArmadaNameReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2691
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private bool? __pbn__IsScoinLack;

	private bool? __pbn__IsHcoinLack;

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

	[ProtoMember(2, Name = "is_scoin_lack")]
	public bool IsScoinLack
	{
		get
		{
			return __pbn__IsScoinLack.GetValueOrDefault();
		}
		set
		{
			__pbn__IsScoinLack = value;
		}
	}

	[ProtoMember(3, Name = "is_hcoin_lack")]
	public bool IsHcoinLack
	{
		get
		{
			return __pbn__IsHcoinLack.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHcoinLack = value;
		}
	}

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

	public bool ShouldSerializeIsScoinLack()
	{
		return __pbn__IsScoinLack.HasValue;
	}

	public void ResetIsScoinLack()
	{
		__pbn__IsScoinLack = null;
	}

	public bool ShouldSerializeIsHcoinLack()
	{
		return __pbn__IsHcoinLack.HasValue;
	}

	public void ResetIsHcoinLack()
	{
		__pbn__IsHcoinLack = null;
	}
}
