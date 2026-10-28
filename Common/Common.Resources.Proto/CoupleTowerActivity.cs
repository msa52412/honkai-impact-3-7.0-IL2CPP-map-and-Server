using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CoupleTowerActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MaxFloor;

	private bool? __pbn__IsSettled;

	[ProtoMember(1, Name = "max_floor")]
	public uint MaxFloor
	{
		get
		{
			return __pbn__MaxFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFloor = value;
		}
	}

	[ProtoMember(2, Name = "floor_score_list")]
	public List<CoupleTowerScore> FloorScoreLists { get; } = new List<CoupleTowerScore>();


	[ProtoMember(3, Name = "is_settled")]
	public bool IsSettled
	{
		get
		{
			return __pbn__IsSettled.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSettled = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaxFloor()
	{
		return __pbn__MaxFloor.HasValue;
	}

	public void ResetMaxFloor()
	{
		__pbn__MaxFloor = null;
	}

	public bool ShouldSerializeIsSettled()
	{
		return __pbn__IsSettled.HasValue;
	}

	public void ResetIsSettled()
	{
		__pbn__IsSettled = null;
	}
}
