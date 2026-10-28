using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelContext : IExtensible
{
	[ProtoContract]
	public enum ContextType
	{
		[ProtoEnum(Name = "INVALID")]
		Invalid,
		[ProtoEnum(Name = "NORMAL")]
		Normal,
		[ProtoEnum(Name = "ENDLESS")]
		Endless,
		[ProtoEnum(Name = "OPENWORLD_ENDLESS")]
		OpenworldEndless,
		[ProtoEnum(Name = "EXBOSS")]
		Exboss
	}

	private IExtension __pbn__extensionData;

	private ContextType? __pbn__Type;

	private string __pbn__ContextString;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ContextType.Invalid)]
	public ContextType Type
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

	[ProtoMember(2, Name = "context_string")]
	[DefaultValue("")]
	public string ContextString
	{
		get
		{
			return __pbn__ContextString ?? "";
		}
		set
		{
			__pbn__ContextString = value;
		}
	}

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

	public bool ShouldSerializeContextString()
	{
		return __pbn__ContextString != null;
	}

	public void ResetContextString()
	{
		__pbn__ContextString = null;
	}
}
