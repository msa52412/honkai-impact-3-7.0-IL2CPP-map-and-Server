using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MedalOpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 452
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_OWN")]
		NoOwn,
		[ProtoEnum(Name = "EXPIRED")]
		Expired,
		[ProtoEnum(Name = "OFF_ID_ERROR")]
		OffIdError
	}

	private IExtension __pbn__extensionData;

	private MedalOpType? __pbn__Op;

	private uint? __pbn__OnId;

	private uint? __pbn__OffId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "op")]
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

	[ProtoMember(3, Name = "on_id")]
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

	[ProtoMember(4, Name = "off_id")]
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
