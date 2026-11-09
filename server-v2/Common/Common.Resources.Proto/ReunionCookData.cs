using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReunionCookData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BookId;

	private uint? __pbn__CookTimes;

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

	[ProtoMember(2, Name = "cook_times")]
	public uint CookTimes
	{
		get
		{
			return __pbn__CookTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__CookTimes = value;
		}
	}

	[ProtoMember(3, Name = "recycle_material_list")]
	public List<Material> RecycleMaterialLists { get; } = new List<Material>();


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

	public bool ShouldSerializeCookTimes()
	{
		return __pbn__CookTimes.HasValue;
	}

	public void ResetCookTimes()
	{
		__pbn__CookTimes = null;
	}
}
