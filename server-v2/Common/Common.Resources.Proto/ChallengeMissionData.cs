using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChallengeMissionData : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsUnlock;

	[ProtoMember(1, Name = "is_unlock")]
	public bool IsUnlock
	{
		get
		{
			return __pbn__IsUnlock.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUnlock = value;
		}
	}

	[ProtoMember(2, Name = "step_list")]
	public List<ChallengeMissionStep> StepLists { get; } = new List<ChallengeMissionStep>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsUnlock()
	{
		return __pbn__IsUnlock.HasValue;
	}

	public void ResetIsUnlock()
	{
		__pbn__IsUnlock = null;
	}
}
