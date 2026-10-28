using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayReportFileResultRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5413
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_MATCH")]
		NotMatch
	}

	private IExtension __pbn__extensionData;

	private ulong? __pbn__ReplayIdU64;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "replay_id_u64")]
	public ulong ReplayIdU64
	{
		get
		{
			return __pbn__ReplayIdU64.GetValueOrDefault();
		}
		set
		{
			__pbn__ReplayIdU64 = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReplayIdU64()
	{
		return __pbn__ReplayIdU64.HasValue;
	}

	public void ResetReplayIdU64()
	{
		__pbn__ReplayIdU64 = null;
	}
}
