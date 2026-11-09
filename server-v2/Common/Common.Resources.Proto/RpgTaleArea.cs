using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleArea : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	[ProtoMember(1, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}
}
