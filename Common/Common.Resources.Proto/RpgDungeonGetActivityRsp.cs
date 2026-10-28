using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgDungeonGetActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5501
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

	private uint? __pbn__DungeonId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity")]
	public RpgDungeonActivity Activity { get; set; }

	[ProtoMember(3, Name = "dungeon_id")]
	public uint DungeonId
	{
		get
		{
			return __pbn__DungeonId.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDungeonId()
	{
		return __pbn__DungeonId.HasValue;
	}

	public void ResetDungeonId()
	{
		__pbn__DungeonId = null;
	}
}
