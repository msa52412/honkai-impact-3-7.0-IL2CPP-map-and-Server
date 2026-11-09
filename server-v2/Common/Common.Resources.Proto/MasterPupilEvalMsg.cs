using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilEvalMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "pupil_list")]
	public List<PupilMainData> PupilLists { get; } = new List<PupilMainData>();


	[ProtoMember(2, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
