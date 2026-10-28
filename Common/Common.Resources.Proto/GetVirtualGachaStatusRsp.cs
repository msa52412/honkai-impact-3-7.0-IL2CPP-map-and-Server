using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetVirtualGachaStatusRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3511
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

	private uint? __pbn__GroupId;

	private uint? __pbn__CandidateGachaBoxId;

	private uint? __pbn__CandidateGachaPoolId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "gacha_list")]
	public List<VirtualGacha> GachaLists { get; } = new List<VirtualGacha>();


	[ProtoMember(3, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(4, Name = "candidate_gacha_box_id")]
	public uint CandidateGachaBoxId
	{
		get
		{
			return __pbn__CandidateGachaBoxId.GetValueOrDefault();
		}
		set
		{
			__pbn__CandidateGachaBoxId = value;
		}
	}

	[ProtoMember(5, Name = "candidate_item_id_list")]
	public uint[] CandidateItemIdLists { get; set; }

	[ProtoMember(6, Name = "candidate_gacha_pool_id")]
	public uint CandidateGachaPoolId
	{
		get
		{
			return __pbn__CandidateGachaPoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__CandidateGachaPoolId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeCandidateGachaBoxId()
	{
		return __pbn__CandidateGachaBoxId.HasValue;
	}

	public void ResetCandidateGachaBoxId()
	{
		__pbn__CandidateGachaBoxId = null;
	}

	public bool ShouldSerializeCandidateGachaPoolId()
	{
		return __pbn__CandidateGachaPoolId.HasValue;
	}

	public void ResetCandidateGachaPoolId()
	{
		__pbn__CandidateGachaPoolId = null;
	}
}
