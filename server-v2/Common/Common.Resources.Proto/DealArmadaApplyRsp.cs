using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DealArmadaApplyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2610
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "NOT_GRANTED")]
		NotGranted,
		[ProtoEnum(Name = "NOT_TARGET")]
		NotTarget,
		[ProtoEnum(Name = "ARMADA_FULL")]
		ArmadaFull
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private bool? __pbn__Agree;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(3, Name = "agree")]
	public bool Agree
	{
		get
		{
			return __pbn__Agree.GetValueOrDefault();
		}
		set
		{
			__pbn__Agree = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeAgree()
	{
		return __pbn__Agree.HasValue;
	}

	public void ResetAgree()
	{
		__pbn__Agree = null;
	}
}
