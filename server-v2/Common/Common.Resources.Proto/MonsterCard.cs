using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonsterCard : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__Level;

	private uint? __pbn__Star;

	private uint? __pbn__FragmentNum;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(4, Name = "fragment_num")]
	public uint FragmentNum
	{
		get
		{
			return __pbn__FragmentNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FragmentNum = value;
		}
	}

	[ProtoMember(5, Name = "talent_list")]
	public uint[] TalentLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeFragmentNum()
	{
		return __pbn__FragmentNum.HasValue;
	}

	public void ResetFragmentNum()
	{
		__pbn__FragmentNum = null;
	}
}
