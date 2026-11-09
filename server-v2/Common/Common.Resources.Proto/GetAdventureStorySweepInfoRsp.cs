using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetAdventureStorySweepInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3915
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

	private uint? __pbn__UsedStamina;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "story_sweep_list")]
	public List<IslandStorySweepData> StorySweepLists { get; } = new List<IslandStorySweepData>();


	[ProtoMember(3, Name = "used_stamina")]
	public uint UsedStamina
	{
		get
		{
			return __pbn__UsedStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__UsedStamina = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUsedStamina()
	{
		return __pbn__UsedStamina.HasValue;
	}

	public void ResetUsedStamina()
	{
		__pbn__UsedStamina = null;
	}
}
