using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetClientMailDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3801
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "POS_INVALID")]
		PosInvalid,
		[ProtoEnum(Name = "REPEATED_REQ")]
		RepeatedReq
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsEnd;

	private uint? __pbn__Start;

	private ClientMailFilterType? __pbn__FilterType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "mail_list")]
	public List<ClientMail> MailLists { get; } = new List<ClientMail>();


	[ProtoMember(3, Name = "is_end")]
	public bool IsEnd
	{
		get
		{
			return __pbn__IsEnd.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEnd = value;
		}
	}

	[ProtoMember(5, Name = "start")]
	public uint Start
	{
		get
		{
			return __pbn__Start.GetValueOrDefault();
		}
		set
		{
			__pbn__Start = value;
		}
	}

	[ProtoMember(6, Name = "notice_mail_list")]
	public List<ClientMail> NoticeMailLists { get; } = new List<ClientMail>();


	[ProtoMember(7, Name = "filter_type")]
	[DefaultValue(ClientMailFilterType.ClientMailFilterTypeAll)]
	public ClientMailFilterType FilterType
	{
		get
		{
			return __pbn__FilterType.GetValueOrDefault();
		}
		set
		{
			__pbn__FilterType = value;
		}
	}

	[ProtoMember(8, Name = "client_mail_info")]
	public ClientMailInfo ClientMailInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsEnd()
	{
		return __pbn__IsEnd.HasValue;
	}

	public void ResetIsEnd()
	{
		__pbn__IsEnd = null;
	}

	public bool ShouldSerializeStart()
	{
		return __pbn__Start.HasValue;
	}

	public void ResetStart()
	{
		__pbn__Start = null;
	}

	public bool ShouldSerializeFilterType()
	{
		return __pbn__FilterType.HasValue;
	}

	public void ResetFilterType()
	{
		__pbn__FilterType = null;
	}
}
