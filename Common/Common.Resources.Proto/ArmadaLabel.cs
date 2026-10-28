using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaLabel : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PrefixId;

	private string __pbn__Recruit;

	[ProtoMember(1, Name = "prefix_id")]
	public uint PrefixId
	{
		get
		{
			return __pbn__PrefixId.GetValueOrDefault();
		}
		set
		{
			__pbn__PrefixId = value;
		}
	}

	[ProtoMember(2, Name = "label_id_list")]
	public uint[] LabelIdLists { get; set; }

	[ProtoMember(3, Name = "recruit")]
	[DefaultValue("")]
	public string Recruit
	{
		get
		{
			return __pbn__Recruit ?? "";
		}
		set
		{
			__pbn__Recruit = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePrefixId()
	{
		return __pbn__PrefixId.HasValue;
	}

	public void ResetPrefixId()
	{
		__pbn__PrefixId = null;
	}

	public bool ShouldSerializeRecruit()
	{
		return __pbn__Recruit != null;
	}

	public void ResetRecruit()
	{
		__pbn__Recruit = null;
	}
}
