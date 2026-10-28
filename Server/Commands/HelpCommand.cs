using System;
using System.Text;
using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Commands;

[CommandHandler("help", " - shows help page >:3", CommandType.All, new string[] { })]
internal class HelpCommand : Command
{
	public override void Run(Session session, string[] args)
	{
		RecvChatMsgNotify recvChatMsgNotify = new RecvChatMsgNotify();
		StringBuilder stringBuilder = new StringBuilder("<color=#B00B><size=26>Commands</size></color><size=16><color=#555>\n");
		stringBuilder.Append("免费开源项目，如果你付费购买，那么你被骗了\n");
		stringBuilder.Append("command <required> [optional]\n");
		foreach (Command command in CommandFactory.Commands)
		{
			if (command.CmdType != 0 && command.CmdType != CommandType.Player)
			{
				continue;
			}
			stringBuilder.Append("┝<size=22>" + command.Name + " " + command.Description + "</size>\n");
			if (command.Examples != null)
			{
				string[] examples = command.Examples;
				foreach (string text in examples)
				{
					stringBuilder.Append("│┕" + text + "\n");
				}
			}
		}
		stringBuilder.Append("</color></size>");
		ChatMsg chatMsg = new ChatMsg
		{
			Uid = 0u,
			Nickname = "Ai-chan",
			Time = (uint)Global.GetUnixInSeconds(),
			Msg = stringBuilder.ToString(),
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
				RewriteText = stringBuilder.ToString()
			}
		};
		chatMsg.Content.Items.Add(new ChatMsgItem
		{
			MsgStr = stringBuilder.ToString()
		});
		recvChatMsgNotify.ChatMsgLists.Add(chatMsg);
		session.Send(Packet.FromProto(recvChatMsgNotify, CmdId.RecvChatMsgNotify));
	}

	public override void Run(string[] args)
	{
		foreach (Command command in CommandFactory.Commands)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("      " + command.Name);
			Console.ResetColor();
			Command.c.Trail(command.Description);
		}
	}
}
