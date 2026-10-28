using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayCommonInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__CreateTime;

	private uint? __pbn__StageId;

	private string __pbn__FileMd5;

	private string __pbn__DownloadUrl;

	private string __pbn__Version;

	private uint? __pbn__Timespan;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(3, Name = "create_time")]
	public uint CreateTime
	{
		get
		{
			return __pbn__CreateTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CreateTime = value;
		}
	}

	[ProtoMember(4, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(5, Name = "file_md5")]
	[DefaultValue("")]
	public string FileMd5
	{
		get
		{
			return __pbn__FileMd5 ?? "";
		}
		set
		{
			__pbn__FileMd5 = value;
		}
	}

	[ProtoMember(6, Name = "download_url")]
	[DefaultValue("")]
	public string DownloadUrl
	{
		get
		{
			return __pbn__DownloadUrl ?? "";
		}
		set
		{
			__pbn__DownloadUrl = value;
		}
	}

	[ProtoMember(7, Name = "version")]
	[DefaultValue("")]
	public string Version
	{
		get
		{
			return __pbn__Version ?? "";
		}
		set
		{
			__pbn__Version = value;
		}
	}

	[ProtoMember(8, Name = "timespan")]
	public uint Timespan
	{
		get
		{
			return __pbn__Timespan.GetValueOrDefault();
		}
		set
		{
			__pbn__Timespan = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeCreateTime()
	{
		return __pbn__CreateTime.HasValue;
	}

	public void ResetCreateTime()
	{
		__pbn__CreateTime = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeFileMd5()
	{
		return __pbn__FileMd5 != null;
	}

	public void ResetFileMd5()
	{
		__pbn__FileMd5 = null;
	}

	public bool ShouldSerializeDownloadUrl()
	{
		return __pbn__DownloadUrl != null;
	}

	public void ResetDownloadUrl()
	{
		__pbn__DownloadUrl = null;
	}

	public bool ShouldSerializeVersion()
	{
		return __pbn__Version != null;
	}

	public void ResetVersion()
	{
		__pbn__Version = null;
	}

	public bool ShouldSerializeTimespan()
	{
		return __pbn__Timespan.HasValue;
	}

	public void ResetTimespan()
	{
		__pbn__Timespan = null;
	}
}
