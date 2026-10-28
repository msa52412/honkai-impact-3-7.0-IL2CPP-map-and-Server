using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Replay : IExtensible
{
	private IExtension __pbn__extensionData;

	private ulong? __pbn__ReplayIdU64;

	private ReplayType? __pbn__ReplayType;

	[ProtoMember(1, Name = "replay_id_u64")]
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

	[ProtoMember(2, Name = "replay_type")]
	[DefaultValue(ReplayType.ReplayNone)]
	public ReplayType ReplayType
	{
		get
		{
			return __pbn__ReplayType.GetValueOrDefault();
		}
		set
		{
			__pbn__ReplayType = value;
		}
	}

	[ProtoMember(3, Name = "common_info")]
	public ReplayCommonInfo CommonInfo { get; set; }

	[ProtoMember(4, Name = "ext_info")]
	public ReplayExtInfo ExtInfo { get; set; }

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

	public bool ShouldSerializeReplayType()
	{
		return __pbn__ReplayType.HasValue;
	}

	public void ResetReplayType()
	{
		__pbn__ReplayType = null;
	}
}
