using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarCultivateActivityAreaDropRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__CurDropNum;

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

	[ProtoMember(2, Name = "cur_drop_num")]
	public uint CurDropNum
	{
		get
		{
			return __pbn__CurDropNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CurDropNum = value;
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

	public bool ShouldSerializeCurDropNum()
	{
		return __pbn__CurDropNum.HasValue;
	}

	public void ResetCurDropNum()
	{
		__pbn__CurDropNum = null;
	}
}
