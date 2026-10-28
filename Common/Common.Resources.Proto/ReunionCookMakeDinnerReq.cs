using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReunionCookMakeDinnerReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4382
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BookId;

	private ReunionCookBookType? __pbn__BookType;

	[ProtoMember(1, Name = "book_id")]
	public uint BookId
	{
		get
		{
			return __pbn__BookId.GetValueOrDefault();
		}
		set
		{
			__pbn__BookId = value;
		}
	}

	[ProtoMember(2, Name = "recycle_material_list")]
	public List<Material> RecycleMaterialLists { get; } = new List<Material>();


	[ProtoMember(3, Name = "book_type")]
	[DefaultValue(ReunionCookBookType.ReunionCookBookNormal)]
	public ReunionCookBookType BookType
	{
		get
		{
			return __pbn__BookType ?? ReunionCookBookType.ReunionCookBookNormal;
		}
		set
		{
			__pbn__BookType = value;
		}
	}

	[ProtoMember(4, Name = "cook_data")]
	public List<ReunionCookData> CookDatas { get; } = new List<ReunionCookData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBookId()
	{
		return __pbn__BookId.HasValue;
	}

	public void ResetBookId()
	{
		__pbn__BookId = null;
	}

	public bool ShouldSerializeBookType()
	{
		return __pbn__BookType.HasValue;
	}

	public void ResetBookType()
	{
		__pbn__BookType = null;
	}
}
