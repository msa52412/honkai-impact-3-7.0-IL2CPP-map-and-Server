using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClaimScoinRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3911
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SCOIN_FULL")]
		ScoinFull
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AddScoin;

	private bool? __pbn__IsExtra;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "add_scoin")]
	public uint AddScoin
	{
		get
		{
			return __pbn__AddScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__AddScoin = value;
		}
	}

	[ProtoMember(3, Name = "is_extra")]
	public bool IsExtra
	{
		get
		{
			return __pbn__IsExtra.GetValueOrDefault();
		}
		set
		{
			__pbn__IsExtra = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddScoin()
	{
		return __pbn__AddScoin.HasValue;
	}

	public void ResetAddScoin()
	{
		__pbn__AddScoin = null;
	}

	public bool ShouldSerializeIsExtra()
	{
		return __pbn__IsExtra.HasValue;
	}

	public void ResetIsExtra()
	{
		__pbn__IsExtra = null;
	}
}
