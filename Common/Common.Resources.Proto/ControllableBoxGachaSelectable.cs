using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ControllableBoxGachaSelectableGroup : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__SelectDropId;

	[ProtoMember(1, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(2, Name = "select_drop_id")]
	public uint SelectDropId
	{
		get
		{
			return __pbn__SelectDropId.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectDropId = value;
		}
	}

	[ProtoMember(3, Name = "drop_id_list")]
	public uint[] DropIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeSelectDropId()
	{
		return __pbn__SelectDropId.HasValue;
	}

	public void ResetSelectDropId()
	{
		__pbn__SelectDropId = null;
	}
}
