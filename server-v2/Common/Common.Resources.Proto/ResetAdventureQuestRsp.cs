using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ResetAdventureQuestRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3925
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_QUEST")]
		NoQuest,
		[ProtoEnum(Name = "NO_TIMES")]
		NoTimes,
		[ProtoEnum(Name = "HAS_FINISHED")]
		HasFinished,
		[ProtoEnum(Name = "HAS_SELECTED")]
		HasSelected
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}
}
