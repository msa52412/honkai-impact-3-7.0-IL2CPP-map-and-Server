using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCStatus : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScDlcFeverScore;

	private uint? __pbn__ScDlcClimaxScore;

	[ProtoMember(1, Name = "sc_dlc_fever_score")]
	public uint ScDlcFeverScore
	{
		get
		{
			return __pbn__ScDlcFeverScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcFeverScore = value;
		}
	}

	[ProtoMember(2, Name = "sc_dlc_climax_score")]
	public uint ScDlcClimaxScore
	{
		get
		{
			return __pbn__ScDlcClimaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcClimaxScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScDlcFeverScore()
	{
		return __pbn__ScDlcFeverScore.HasValue;
	}

	public void ResetScDlcFeverScore()
	{
		__pbn__ScDlcFeverScore = null;
	}

	public bool ShouldSerializeScDlcClimaxScore()
	{
		return __pbn__ScDlcClimaxScore.HasValue;
	}

	public void ResetScDlcClimaxScore()
	{
		__pbn__ScDlcClimaxScore = null;
	}
}
