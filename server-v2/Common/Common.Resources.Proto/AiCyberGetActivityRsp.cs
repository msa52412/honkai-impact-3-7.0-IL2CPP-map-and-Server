using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberGetActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6651
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

	private string __pbn__Bless;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "ai_cyber_activity")]
	public AiCyberActivity AiCyberActivity { get; set; }

	[ProtoMember(3, Name = "bless")]
	[DefaultValue("")]
	public string Bless
	{
		get
		{
			return __pbn__Bless ?? "";
		}
		set
		{
			__pbn__Bless = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBless()
	{
		return __pbn__Bless != null;
	}

	public void ResetBless()
	{
		__pbn__Bless = null;
	}
}
