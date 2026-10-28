using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOpenworldEndlessDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3419
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	private EndlessType? __pbn__Type;

	private bool? __pbn__IsFinishFront;

	[ProtoMember(1, Name = "level")]
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

	[ProtoMember(2, Name = "type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "is_finish_front")]
	public bool IsFinishFront
	{
		get
		{
			return __pbn__IsFinishFront.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinishFront = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeIsFinishFront()
	{
		return __pbn__IsFinishFront.HasValue;
	}

	public void ResetIsFinishFront()
	{
		__pbn__IsFinishFront = null;
	}
}
