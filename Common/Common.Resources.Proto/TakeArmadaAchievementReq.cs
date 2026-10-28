using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeArmadaAchievementReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2801
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private bool? __pbn__IsTakeAll;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "is_take_all")]
	public bool IsTakeAll
	{
		get
		{
			return __pbn__IsTakeAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeIsTakeAll()
	{
		return __pbn__IsTakeAll.HasValue;
	}

	public void ResetIsTakeAll()
	{
		__pbn__IsTakeAll = null;
	}
}
