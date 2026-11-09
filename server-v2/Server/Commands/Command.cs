using System;
using Common.Utils;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands;

public abstract class Command
{
	public static readonly Logger c = new Logger("Command", ConsoleColor.Cyan, traceOnError: false);

	public string Name = string.Empty;

	public string Description = string.Empty;

	public string[] Examples = Array.Empty<string>();

	public CommandType CmdType = CommandType.Player;

	public virtual void Run(Session session, string[] args)
	{
		throw new NotImplementedException();
	}

	public virtual void Run(Player player, string[] args)
	{
		throw new NotImplementedException();
	}

	public virtual void Run(string[] args)
	{
		throw new NotImplementedException();
	}
}
