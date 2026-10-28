using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGachaLogRsp : IExtensible
{
	[ProtoContract]
	public class GachaLogInfo : IExtensible
	{
		private IExtension __pbn__extensionData;

		private GachaType? __pbn__GachaType;

		[ProtoMember(1, Name = "gacha_type")]
		[DefaultValue(GachaType.GachaHcoin)]
		public GachaType GachaType
		{
			get
			{
				return __pbn__GachaType ?? GachaType.GachaHcoin;
			}
			set
			{
				__pbn__GachaType = value;
			}
		}

		[ProtoMember(2, Name = "log_list")]
		public List<GachaLog> LogLists { get; } = new List<GachaLog>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeGachaType()
		{
			return __pbn__GachaType.HasValue;
		}

		public void ResetGachaType()
		{
			__pbn__GachaType = null;
		}
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4707
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

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "log_info_list")]
	public List<GachaLogInfo> LogInfoLists { get; } = new List<GachaLogInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
