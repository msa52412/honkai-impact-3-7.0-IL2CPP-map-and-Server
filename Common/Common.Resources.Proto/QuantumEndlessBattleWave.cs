using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QuantumEndlessBattleWave : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__WaveId;

	private StageEndStatus? __pbn__EndStatus;

	private uint? __pbn__CostTime;

	[ProtoMember(1, Name = "wave_id")]
	public uint WaveId
	{
		get
		{
			return __pbn__WaveId.GetValueOrDefault();
		}
		set
		{
			__pbn__WaveId = value;
		}
	}

	[ProtoMember(2, Name = "end_status")]
	[DefaultValue(StageEndStatus.StageWin)]
	public StageEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? StageEndStatus.StageWin;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(3, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWaveId()
	{
		return __pbn__WaveId.HasValue;
	}

	public void ResetWaveId()
	{
		__pbn__WaveId = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}
}
