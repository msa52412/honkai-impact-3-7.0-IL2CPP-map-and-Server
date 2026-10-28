using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyUseItemReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3858
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__ItemId;

	private uint? __pbn__Para;

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

	[ProtoMember(2, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(3, Name = "para")]
	public uint Para
	{
		get
		{
			return __pbn__Para.GetValueOrDefault();
		}
		set
		{
			__pbn__Para = value;
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

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializePara()
	{
		return __pbn__Para.HasValue;
	}

	public void ResetPara()
	{
		__pbn__Para = null;
	}
}
