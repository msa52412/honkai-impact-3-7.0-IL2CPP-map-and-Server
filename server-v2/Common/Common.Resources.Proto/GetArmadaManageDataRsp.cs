using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaManageDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2612
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

	private uint? __pbn__ApplyLevel;

	private bool? __pbn__AutoPass;

	private uint? __pbn__AutoPassLevel;

	private uint? __pbn__BuildOwner;

	private uint? __pbn__BuildOwnershipTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "apply_list")]
	public List<ArmadaApply> ApplyLists { get; } = new List<ArmadaApply>();


	[ProtoMember(3, Name = "apply_result_list")]
	public List<ArmadaApplyResult> ApplyResultLists { get; } = new List<ArmadaApplyResult>();


	[ProtoMember(4, Name = "apply_level")]
	public uint ApplyLevel
	{
		get
		{
			return __pbn__ApplyLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ApplyLevel = value;
		}
	}

	[ProtoMember(5, Name = "auto_pass")]
	public bool AutoPass
	{
		get
		{
			return __pbn__AutoPass.GetValueOrDefault();
		}
		set
		{
			__pbn__AutoPass = value;
		}
	}

	[ProtoMember(6, Name = "auto_pass_level")]
	public uint AutoPassLevel
	{
		get
		{
			return __pbn__AutoPassLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AutoPassLevel = value;
		}
	}

	[ProtoMember(7, Name = "build_owner")]
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

	[ProtoMember(8, Name = "build_ownership_time")]
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

	public bool ShouldSerializeApplyLevel()
	{
		return __pbn__ApplyLevel.HasValue;
	}

	public void ResetApplyLevel()
	{
		__pbn__ApplyLevel = null;
	}

	public bool ShouldSerializeAutoPass()
	{
		return __pbn__AutoPass.HasValue;
	}

	public void ResetAutoPass()
	{
		__pbn__AutoPass = null;
	}

	public bool ShouldSerializeAutoPassLevel()
	{
		return __pbn__AutoPassLevel.HasValue;
	}

	public void ResetAutoPassLevel()
	{
		__pbn__AutoPassLevel = null;
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
