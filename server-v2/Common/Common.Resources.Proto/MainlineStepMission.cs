using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MainlineStepMission : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsUpdate;

	[ProtoMember(1, Name = "is_update")]
	public bool IsUpdate
	{
		get
		{
			return __pbn__IsUpdate.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUpdate = value;
		}
	}

	[ProtoMember(2, Name = "cur_mainline_step_list")]
	public uint[] CurMainlineStepLists { get; set; }

	[ProtoMember(3, Name = "finished_mainline_step_list")]
	public uint[] FinishedMainlineStepLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsUpdate()
	{
		return __pbn__IsUpdate.HasValue;
	}

	public void ResetIsUpdate()
	{
		__pbn__IsUpdate = null;
	}
}
