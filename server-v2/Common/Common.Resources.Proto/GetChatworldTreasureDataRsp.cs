using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetChatworldTreasureDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2416
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_CHATWORLD")]
		NotInChatworld
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(3, Name = "chatworld_treasure_list")]
	public List<ChatworldTreasure> ChatworldTreasureLists { get; } = new List<ChatworldTreasure>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}
}
