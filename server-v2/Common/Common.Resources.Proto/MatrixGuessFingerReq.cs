using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixGuessFingerReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2349
	}

	private IExtension __pbn__extensionData;

	private MatrixFingerType? __pbn__FingerType;

	[ProtoMember(1, Name = "finger_type")]
	[DefaultValue(MatrixFingerType.MatrixFingerNone)]
	public MatrixFingerType FingerType
	{
		get
		{
			return __pbn__FingerType.GetValueOrDefault();
		}
		set
		{
			__pbn__FingerType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFingerType()
	{
		return __pbn__FingerType.HasValue;
	}

	public void ResetFingerType()
	{
		__pbn__FingerType = null;
	}
}
