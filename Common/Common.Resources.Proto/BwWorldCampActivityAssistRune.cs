using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BwWorldCampActivityAssistRune : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "producer_uid")]
	public uint ProducerUid { get; set; }

	[ProtoMember(2, Name = "producer_nickname")]
	[DefaultValue("")]
	public string ProducerNickname { get; set; } = "";


	[ProtoMember(3, Name = "producer_head_id")]
	public uint ProducerHeadId { get; set; }

	[ProtoMember(4, Name = "producer_frame_id")]
	public uint ProducerFrameId { get; set; }

	[ProtoMember(5, Name = "rune")]
	public ChapterBwWorldRune Rune { get; set; }

	[ProtoMember(6, Name = "npc_id")]
	public uint NpcId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
