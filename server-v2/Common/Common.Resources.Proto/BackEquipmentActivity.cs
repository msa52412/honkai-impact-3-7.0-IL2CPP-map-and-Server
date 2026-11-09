using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BackEquipmentActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RemainBackTimes;

	[ProtoMember(1, Name = "mainid_list")]
	public uint[] MainidLists { get; set; }

	[ProtoMember(2, Name = "remain_back_times")]
	public uint RemainBackTimes
	{
		get
		{
			return __pbn__RemainBackTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainBackTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRemainBackTimes()
	{
		return __pbn__RemainBackTimes.HasValue;
	}

	public void ResetRemainBackTimes()
	{
		__pbn__RemainBackTimes = null;
	}
}
