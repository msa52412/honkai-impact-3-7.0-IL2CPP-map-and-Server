using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMainStoryBlessRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1704
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

	private string __pbn__SelfBless;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "bless_list")]
	public List<string> BlessLists { get; } = new List<string>();


	[ProtoMember(3, Name = "self_bless")]
	[DefaultValue("")]
	public string SelfBless
	{
		get
		{
			return __pbn__SelfBless ?? "";
		}
		set
		{
			__pbn__SelfBless = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSelfBless()
	{
		return __pbn__SelfBless != null;
	}

	public void ResetSelfBless()
	{
		__pbn__SelfBless = null;
	}
}
