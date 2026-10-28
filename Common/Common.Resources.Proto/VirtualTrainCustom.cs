using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualTrainCustom : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CustomId;

	private uint? __pbn__Level;

	private uint? __pbn__ChoosedIndex;

	private uint? __pbn__MaxIndex;

	[ProtoMember(1, Name = "custom_id")]
	public uint CustomId
	{
		get
		{
			return __pbn__CustomId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomId = value;
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

	[ProtoMember(3, Name = "choosed_index")]
	public uint ChoosedIndex
	{
		get
		{
			return __pbn__ChoosedIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__ChoosedIndex = value;
		}
	}

	[ProtoMember(4, Name = "max_index")]
	public uint MaxIndex
	{
		get
		{
			return __pbn__MaxIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxIndex = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCustomId()
	{
		return __pbn__CustomId.HasValue;
	}

	public void ResetCustomId()
	{
		__pbn__CustomId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeChoosedIndex()
	{
		return __pbn__ChoosedIndex.HasValue;
	}

	public void ResetChoosedIndex()
	{
		__pbn__ChoosedIndex = null;
	}

	public bool ShouldSerializeMaxIndex()
	{
		return __pbn__MaxIndex.HasValue;
	}

	public void ResetMaxIndex()
	{
		__pbn__MaxIndex = null;
	}
}
