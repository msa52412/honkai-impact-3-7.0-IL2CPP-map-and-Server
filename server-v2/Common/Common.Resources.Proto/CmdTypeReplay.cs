using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeReplay
{
	[ProtoEnum(Name = "CMD_REPLAY_BEGIN_REQ")]
	CmdReplayBeginReq = 5400,
	[ProtoEnum(Name = "CMD_REPLAY_BEGIN_RSP")]
	CmdReplayBeginRsp,
	[ProtoEnum(Name = "CMD_REPLAY_END_REQ")]
	CmdReplayEndReq,
	[ProtoEnum(Name = "CMD_REPLAY_END_RSP")]
	CmdReplayEndRsp,
	[ProtoEnum(Name = "CMD_REPORT_REPLAY_FRAME_REQ")]
	CmdReportReplayFrameReq,
	[ProtoEnum(Name = "CMD_REPORT_REPLAY_FRAME_RSP")]
	CmdReportReplayFrameRsp,
	[ProtoEnum(Name = "CMD_WATCH_REPLAY_REQ")]
	CmdWatchReplayReq,
	[ProtoEnum(Name = "CMD_WATCH_REPLAY_RSP")]
	CmdWatchReplayRsp,
	[ProtoEnum(Name = "CMD_REPLAY_FRAME_NOTIFY")]
	CmdReplayFrameNotify,
	[ProtoEnum(Name = "CMD_REPLAY_FRAME_END_NOTIFY")]
	CmdReplayFrameEndNotify,
	[ProtoEnum(Name = "CMD_REPLAY_GET_FILE_TOKEN_REQ")]
	CmdReplayGetFileTokenReq,
	[ProtoEnum(Name = "CMD_REPLAY_GET_FILE_TOKEN_RSP")]
	CmdReplayGetFileTokenRsp,
	[ProtoEnum(Name = "CMD_REPLAY_REPORT_FILE_RESULT_REQ")]
	CmdReplayReportFileResultReq,
	[ProtoEnum(Name = "CMD_REPLAY_REPORT_FILE_RESULT_RSP")]
	CmdReplayReportFileResultRsp,
	[ProtoEnum(Name = "CMD_REPLAY_GET_UPLOAD_LIST_REQ")]
	CmdReplayGetUploadListReq,
	[ProtoEnum(Name = "CMD_REPLAY_GET_UPLOAD_LIST_RSP")]
	CmdReplayGetUploadListRsp,
	[ProtoEnum(Name = "CMD_REPLAY_GET_RECOMMEND_LIST_REQ")]
	CmdReplayGetRecommendListReq,
	[ProtoEnum(Name = "CMD_REPLAY_GET_RECOMMEND_LIST_RSP")]
	CmdReplayGetRecommendListRsp,
	[ProtoEnum(Name = "CMD_REPLAY_GET_RECOMMEND_BRIEF_REQ")]
	CmdReplayGetRecommendBriefReq,
	[ProtoEnum(Name = "CMD_REPLAY_GET_RECOMMEND_BRIEF_RSP")]
	CmdReplayGetRecommendBriefRsp
}
