using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMinuteStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 543
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

	private bool? __pbn__IsOpen;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "is_open")]
	public bool IsOpen
	{
		get
		{
			return __pbn__IsOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpen = value;
		}
	}

	[ProtoMember(3, Name = "stage_list")]
	public List<MinuteStage> StageLists { get; } = new List<MinuteStage>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsOpen()
	{
		return __pbn__IsOpen.HasValue;
	}

	public void ResetIsOpen()
	{
		__pbn__IsOpen = null;
	}
}
