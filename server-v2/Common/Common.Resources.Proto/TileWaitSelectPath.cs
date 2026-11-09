using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileWaitSelectPath : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LeftStepNum;

	[ProtoMember(1, Name = "candidate_path_cell_id_list")]
	public uint[] CandidatePathCellIdLists { get; set; }

	[ProtoMember(2, Name = "left_step_num")]
	public uint LeftStepNum
	{
		get
		{
			return __pbn__LeftStepNum.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftStepNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLeftStepNum()
	{
		return __pbn__LeftStepNum.HasValue;
	}

	public void ResetLeftStepNum()
	{
		__pbn__LeftStepNum = null;
	}
}
