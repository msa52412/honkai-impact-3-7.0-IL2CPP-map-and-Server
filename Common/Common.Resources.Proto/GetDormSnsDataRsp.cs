using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDormSnsDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 638
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BeLikedNum;

	private uint? __pbn__BeVisitNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "be_liked_num")]
	public uint BeLikedNum
	{
		get
		{
			return __pbn__BeLikedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BeLikedNum = value;
		}
	}

	[ProtoMember(3, Name = "record_list")]
	public List<DormVisitRecord> RecordLists { get; } = new List<DormVisitRecord>();


	[ProtoMember(4, Name = "be_visit_num")]
	public uint BeVisitNum
	{
		get
		{
			return __pbn__BeVisitNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BeVisitNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeLikedNum()
	{
		return __pbn__BeLikedNum.HasValue;
	}

	public void ResetBeLikedNum()
	{
		__pbn__BeLikedNum = null;
	}

	public bool ShouldSerializeBeVisitNum()
	{
		return __pbn__BeVisitNum.HasValue;
	}

	public void ResetBeVisitNum()
	{
		__pbn__BeVisitNum = null;
	}
}
