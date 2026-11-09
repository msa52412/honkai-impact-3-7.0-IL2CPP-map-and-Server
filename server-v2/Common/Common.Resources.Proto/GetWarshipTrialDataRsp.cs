using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWarshipTrialDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5453
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsAll;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "trial_warship_list")]
	public List<TrialWarship> TrialWarshipLists { get; } = new List<TrialWarship>();


	[ProtoMember(3, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}
}
