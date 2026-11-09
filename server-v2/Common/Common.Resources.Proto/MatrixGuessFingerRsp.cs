using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixGuessFingerRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2350
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_WIN")]
		NotWin
	}

	private IExtension __pbn__extensionData;

	private MatrixFingerType? __pbn__PlayerFingerType;

	private MatrixFingerType? __pbn__RobotFingerType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "player_finger_type")]
	[DefaultValue(MatrixFingerType.MatrixFingerNone)]
	public MatrixFingerType PlayerFingerType
	{
		get
		{
			return __pbn__PlayerFingerType.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerFingerType = value;
		}
	}

	[ProtoMember(3, Name = "robot_finger_type")]
	[DefaultValue(MatrixFingerType.MatrixFingerNone)]
	public MatrixFingerType RobotFingerType
	{
		get
		{
			return __pbn__RobotFingerType.GetValueOrDefault();
		}
		set
		{
			__pbn__RobotFingerType = value;
		}
	}

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePlayerFingerType()
	{
		return __pbn__PlayerFingerType.HasValue;
	}

	public void ResetPlayerFingerType()
	{
		__pbn__PlayerFingerType = null;
	}

	public bool ShouldSerializeRobotFingerType()
	{
		return __pbn__RobotFingerType.HasValue;
	}

	public void ResetRobotFingerType()
	{
		__pbn__RobotFingerType = null;
	}
}
