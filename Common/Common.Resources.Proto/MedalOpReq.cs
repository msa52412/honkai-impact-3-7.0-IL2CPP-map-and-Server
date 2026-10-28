using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MedalOpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 451
	}

	private IExtension __pbn__extensionData;

	private MedalOpType? __pbn__Op;

	private uint? __pbn__OnId;

	private uint? __pbn__OffId;

	[ProtoMember(1, Name = "op")]
	[DefaultValue(MedalOpType.MedalWear)]
	public MedalOpType Op
	{
		get
		{
			return __pbn__Op ?? MedalOpType.MedalWear;
		}
		set
		{
			__pbn__Op = value;
		}
	}

	[ProtoMember(2, Name = "on_id")]
	public uint OnId
	{
		get
		{
			return __pbn__OnId.GetValueOrDefault();
		}
		set
		{
			__pbn__OnId = value;
		}
	}

	[ProtoMember(3, Name = "off_id")]
	public uint OffId
	{
		get
		{
			return __pbn__OffId.GetValueOrDefault();
		}
		set
		{
			__pbn__OffId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOp()
	{
		return __pbn__Op.HasValue;
	}

	public void ResetOp()
	{
		__pbn__Op = null;
	}

	public bool ShouldSerializeOnId()
	{
		return __pbn__OnId.HasValue;
	}

	public void ResetOnId()
	{
		__pbn__OnId = null;
	}

	public bool ShouldSerializeOffId()
	{
		return __pbn__OffId.HasValue;
	}

	public void ResetOffId()
	{
		__pbn__OffId = null;
	}
}
