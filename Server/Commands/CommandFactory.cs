using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Common.Utils;

namespace PemukulPaku.GameServer.Commands;

public static class CommandFactory
{
	public static readonly List<Command> Commands = new List<Command>();

	private static readonly Logger c = new Logger("Factory", ConsoleColor.Yellow);

	public static void LoadCommandHandlers()
	{
		c.Log("Loading Command Handlers...");
		IEnumerable<Type> source = from t in Assembly.GetExecutingAssembly().GetTypes()
			select (t);
		foreach (var item3 in from Type t in source.ToList()
			let attrs = (Attribute[])t.GetCustomAttributes(typeof(CommandHandler), inherit: false)
			where attrs.Length != 0
			let attr = (CommandHandler)attrs[0]
			select (t, attr))
		{
			Type item = item3.Item1;
			CommandHandler item2 = item3.Item2;
			Command command = (Command)Activator.CreateInstance(item);
			if (command != null)
			{
				command.Name = item2.Name;
				command.Description = item2.Description;
				command.CmdType = item2.CmdType;
				command.Examples = item2.Examples;
				Commands.Add(command);
				Logger logger = c;
				string[] array = new string[1];
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Loaded Command Handler ");
				defaultInterpolatedStringHandler.AppendFormatted(item.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" for Command \"");
				defaultInterpolatedStringHandler.AppendFormatted(command.Name);
				defaultInterpolatedStringHandler.AppendLiteral("\"");
				array[0] = defaultInterpolatedStringHandler.ToStringAndClear();
				logger.Log(array);
			}
		}
		c.Log("Finished Loading Commands");
	}
}
