using System;
using System.Collections.Generic;
using System.Linq;

namespace PemukulPaku.GameServer.Commands;

public class ReadLine
{
	public Session? session = null;

	public static ReadLine? Instance { get; private set; }

	public static ReadLine GetInstance()
	{
		return Instance ?? (Instance = new ReadLine());
	}

	public void Start()
	{
		while (true)
		{
			string text = Console.ReadLine();
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			List<string> args = text.Split(' ').ToList();
			Command command = CommandFactory.Commands.Find((Command cmd) => args[0] == cmd.Name.ToLower());
			if (command != null)
			{
				args.RemoveAt(0);
				try
				{
					if (command.CmdType == CommandType.All || command.CmdType == CommandType.Console)
					{
						command.Run(args.ToArray());
					}
					else if (session != null)
					{
						command.Run(session, args.ToArray());
						Command.c.Log("Command executed");
					}
					else
					{
						Command.c.Error("Invalid usage, try selecting session first with target");
					}
				}
				catch (Exception ex)
				{
					Command.c.Error(ex.Message);
				}
			}
			else
			{
				Command.c.Error("Command not found, try using help");
			}
		}
	}
}
