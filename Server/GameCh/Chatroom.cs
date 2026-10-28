using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Commands;

namespace PemukulPaku.GameServer.Game.Chatrooms;

public class Chatroom
{
	public readonly uint Id;

	public readonly List<Session> Members = new List<Session>();

	public readonly List<ChatMsg> Messages = new List<ChatMsg>();

	public Chatroom(uint id)
	{
		Id = id;
	}

	public void Join(in Session session)
	{
		Members.Add(session);
	}

	public void OnSendChat(in Session session, ChatMsg chatMsg)
	{
		string text = chatMsg.Content.Items.Where((ChatMsgItem item) => item.MsgStr != null).FirstOrDefault()?.MsgStr;
		if (text != null)
		{
			List<string> args = text.Split(' ').ToList();
			Command command = CommandFactory.Commands.Find((Command cmd) => args[0] == cmd.Name.ToLower());
			if (command != null)
			{
				args.RemoveAt(0);
				try
				{
					if (command.CmdType == CommandType.All || command.CmdType == CommandType.Player)
					{
						command.Run(session, args.ToArray());
						if (command.CmdType == CommandType.Player)
						{
							SendAiMsg("Command executed\n免费开源项目，如果你付费购买，那么你被骗了", session);
						}
					}
					else
					{
						SendAiMsg("Invalid usage", session);
					}
					return;
				}
				catch (Exception ex)
				{
					SendAiMsg(ex.Message, session);
					return;
				}
			}
			chatMsg.CheckResult = new ChatMsgSensitiveCheckResult
			{
				ShieldType = 0,
				NumberCheck = 0,
				RewriteText = text
			};
			chatMsg.Msg = text;
		}
		UserScheme User = session.Player.User;
		chatMsg.Uid = User.Uid;
		chatMsg.Nickname = User.Nick;
		chatMsg.Time = (uint)Global.GetUnixInSeconds();
		chatMsg.AvatarId = (uint)User.AssistantAvatarId;
		chatMsg.DressId = session.Player.AvatarList.Where((AvatarScheme avatar) => avatar.AvatarId == User.AssistantAvatarId).First().DressId;
		chatMsg.FrameId = ((User.FrameId < 200001) ? 200001u : ((uint)User.FrameId));
		chatMsg.CustomHeadId = (uint)User.CustomHeadId;
		Broadcast(chatMsg);
		Messages.Add(chatMsg);
	}

	public void Broadcast(ChatMsg chatMsg)
	{
		foreach (Session member in Members)
		{
			RecvChatMsgNotify recvChatMsgNotify = new RecvChatMsgNotify();
			recvChatMsgNotify.ChatMsgLists.Add(chatMsg);
			member.Send(Packet.FromProto(recvChatMsgNotify, CmdId.RecvChatMsgNotify));
		}
	}

	public void SendAiMsg(string msg, Session? session)
	{
		if (session == null)
		{
			foreach (Session member in Members)
			{
				SendAiMsg(msg, member);
			}
			return;
		}
		RecvChatMsgNotify recvChatMsgNotify = new RecvChatMsgNotify();
		ChatMsg chatMsg = new ChatMsg
		{
			Uid = 0u,
			Nickname = "Ai-chan",
			Time = (uint)Global.GetUnixInSeconds(),
			Msg = msg,
			Content = new ChatMsgContent(),
			Channel = ChatMsg.MsgChannel.World,
			AvatarId = 3201u,
			DressId = 593201u,
			FrameId = 200001u,
			CustomHeadId = 161080u,
			CheckResult = new ChatMsgSensitiveCheckResult
			{
				NumberCheck = 0,
				ShieldType = 0,
				RewriteText = msg
			}
		};
		chatMsg.Content.Items.Add(new ChatMsgItem
		{
			MsgStr = msg
		});
		recvChatMsgNotify.ChatMsgLists.Add(chatMsg);
		session!.Send(Packet.FromProto(recvChatMsgNotify, CmdId.RecvChatMsgNotify));
	}
}
