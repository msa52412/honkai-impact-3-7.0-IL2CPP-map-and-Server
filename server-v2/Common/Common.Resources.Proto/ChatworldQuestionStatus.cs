using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldQuestionStatus
{
	[ProtoEnum(Name = "CHATWORLD_QUESTION_NONE")]
	ChatworldQuestionNone,
	[ProtoEnum(Name = "CHATWORLD_QUESTION_PREPARE")]
	ChatworldQuestionPrepare,
	[ProtoEnum(Name = "CHATWORLD_QUESTION_ANSWER")]
	ChatworldQuestionAnswer,
	[ProtoEnum(Name = "CHATWORLD_QUESTION_RESULT")]
	ChatworldQuestionResult,
	[ProtoEnum(Name = "CHATWORLD_QUESTION_CD")]
	ChatworldQuestionCd
}
