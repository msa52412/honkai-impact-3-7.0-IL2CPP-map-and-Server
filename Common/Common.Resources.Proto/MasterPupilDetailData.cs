using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilDetailData : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "master")]
	public MasterPupilData Master { get; set; }

	[ProtoMember(2, Name = "pupil_list")]
	public List<MasterPupilData> PupilLists { get; } = new List<MasterPupilData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
