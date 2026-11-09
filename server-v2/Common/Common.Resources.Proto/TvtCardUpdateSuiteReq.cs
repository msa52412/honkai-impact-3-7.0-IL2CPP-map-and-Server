using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtCardUpdateSuiteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3313
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SelectedSuiteIdx;

	[ProtoMember(1, Name = "suite_list")]
	public List<TvtCardSuite> SuiteLists { get; } = new List<TvtCardSuite>();


	[ProtoMember(2, Name = "selected_suite_idx")]
	public uint SelectedSuiteIdx
	{
		get
		{
			return __pbn__SelectedSuiteIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectedSuiteIdx = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSelectedSuiteIdx()
	{
		return __pbn__SelectedSuiteIdx.HasValue;
	}

	public void ResetSelectedSuiteIdx()
	{
		__pbn__SelectedSuiteIdx = null;
	}
}
