using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WaveRushUpdateBuffSuiteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5967
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SuiteId;

	private string __pbn__SuiteName;

	[ProtoMember(1, Name = "suite_id")]
	public uint SuiteId
	{
		get
		{
			return __pbn__SuiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SuiteId = value;
		}
	}

	[ProtoMember(2, Name = "suite_name")]
	[DefaultValue("")]
	public string SuiteName
	{
		get
		{
			return __pbn__SuiteName ?? "";
		}
		set
		{
			__pbn__SuiteName = value;
		}
	}

	[ProtoMember(3, Name = "buff_id_list")]
	public uint[] BuffIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSuiteId()
	{
		return __pbn__SuiteId.HasValue;
	}

	public void ResetSuiteId()
	{
		__pbn__SuiteId = null;
	}

	public bool ShouldSerializeSuiteName()
	{
		return __pbn__SuiteName != null;
	}

	public void ResetSuiteName()
	{
		__pbn__SuiteName = null;
	}
}
