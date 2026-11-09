using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectAdventureQuestReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3902
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	private uint? __pbn__ElfId;

	[ProtoMember(1, Name = "pool_id")]
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

	[ProtoMember(2, Name = "avatar_list")]
	public uint[] AvatarLists { get; set; }

	[ProtoMember(3, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
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

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}
}
