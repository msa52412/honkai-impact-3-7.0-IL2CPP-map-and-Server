using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMainStoryMemoirsDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1702
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RegisterTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "data_list")]
	public List<MemoirsSpecialData> DataLists { get; } = new List<MemoirsSpecialData>();


	[ProtoMember(3, Name = "register_time")]
	public uint RegisterTime
	{
		get
		{
			return __pbn__RegisterTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RegisterTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRegisterTime()
	{
		return __pbn__RegisterTime.HasValue;
	}

	public void ResetRegisterTime()
	{
		__pbn__RegisterTime = null;
	}
}
