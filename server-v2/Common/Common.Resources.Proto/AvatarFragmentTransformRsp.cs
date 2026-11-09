using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarFragmentTransformRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 828
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NEED_SSS")]
		NeedSss,
		[ProtoEnum(Name = "LACK")]
		Lack,
		[ProtoEnum(Name = "FULL")]
		Full,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReturnId;

	private uint? __pbn__ReturnNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "return_id")]
	public uint ReturnId
	{
		get
		{
			return __pbn__ReturnId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReturnId = value;
		}
	}

	[ProtoMember(3, Name = "return_num")]
	public uint ReturnNum
	{
		get
		{
			return __pbn__ReturnNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ReturnNum = value;
		}
	}

	[ProtoMember(4, Name = "return_list")]
	public List<GenericItemNum> ReturnLists { get; } = new List<GenericItemNum>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReturnId()
	{
		return __pbn__ReturnId.HasValue;
	}

	public void ResetReturnId()
	{
		__pbn__ReturnId = null;
	}

	public bool ShouldSerializeReturnNum()
	{
		return __pbn__ReturnNum.HasValue;
	}

	public void ResetReturnNum()
	{
		__pbn__ReturnNum = null;
	}
}
