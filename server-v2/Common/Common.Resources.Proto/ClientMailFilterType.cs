using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ClientMailFilterType
{
	[ProtoEnum(Name = "CLIENT_MAIL_FILTER_TYPE_ALL")]
	ClientMailFilterTypeAll,
	[ProtoEnum(Name = "CLIENT_MAIL_FILTER_TYPE_IMPORTANT")]
	ClientMailFilterTypeImportant,
	[ProtoEnum(Name = "CLIENT_MAIL_FILTER_TYPE_FAVORITE")]
	ClientMailFilterTypeFavorite
}
