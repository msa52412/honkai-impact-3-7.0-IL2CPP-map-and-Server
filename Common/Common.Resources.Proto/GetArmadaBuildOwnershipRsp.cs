using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaBuildOwnershipRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2686
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_GRANTED")]
		NotGranted,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BuildOwner;

	private uint? __pbn__BuildOwnershipTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "build_owner")]
	public uint BuildOwner
	{
		get
		{
			return __pbn__BuildOwner.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildOwner = value;
		}
	}

	[ProtoMember(3, Name = "build_ownership_time")]
	public uint BuildOwnershipTime
	{
		get
		{
			return __pbn__BuildOwnershipTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildOwnershipTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuildOwner()
	{
		return __pbn__BuildOwner.HasValue;
	}

	public void ResetBuildOwner()
	{
		__pbn__BuildOwner = null;
	}

	public bool ShouldSerializeBuildOwnershipTime()
	{
		return __pbn__BuildOwnershipTime.HasValue;
	}

	public void ResetBuildOwnershipTime()
	{
		__pbn__BuildOwnershipTime = null;
	}
}
