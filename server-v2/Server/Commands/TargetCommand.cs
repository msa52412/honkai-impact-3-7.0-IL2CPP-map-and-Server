using System;
using System.Collections.Generic;

namespace PemukulPaku.GameServer.Commands;

[CommandHandler("target", "[id], displays and selects User sessions for commands", CommandType.Console, new string[] { })]
internal class TargetCommand : Command
{
	public override void Run(string[] args)
	{
		if (args.Length == 0)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Selected Session: ");
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine(ReadLine.GetInstance().session?.Id ?? "None");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Active Sessions:");
			Console.ResetColor();
			{
				foreach (KeyValuePair<string, Session> session in Server.GetInstance().Sessions)
				{
					Command.c.Trail(session.Key);
				}
				return;
			}
		}
		if (Server.GetInstance().Sessions.TryGetValue(args[0], out var value))
		{
			ReadLine.GetInstance().session = value;
			Command.c.Log("Session set to " + value.Id);
		}
		else
		{
			Command.c.Error("Session not found with key " + args[0]);
		}
	}
}
