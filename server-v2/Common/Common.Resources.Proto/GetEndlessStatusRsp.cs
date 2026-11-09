using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetEndlessStatusRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3433
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

	private uint? __pbn__SelectedEndlessType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_status")]
	public EndlessStatus CurStatus { get; set; }

	[ProtoMember(3, Name = "next_status_list")]
	public List<EndlessStatus> NextStatusLists { get; } = new List<EndlessStatus>();


	[ProtoMember(4, Name = "selected_endless_type")]
	public uint SelectedEndlessType
	{
		get
		{
			return __pbn__SelectedEndlessType.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectedEndlessType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSelectedEndlessType()
	{
		return __pbn__SelectedEndlessType.HasValue;
	}

	public void ResetSelectedEndlessType()
	{
		__pbn__SelectedEndlessType = null;
	}
}
