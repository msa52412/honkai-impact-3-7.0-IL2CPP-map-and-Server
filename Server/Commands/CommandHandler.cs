using System;

namespace PemukulPaku.GameServer.Commands;

[AttributeUsage(AttributeTargets.Class)]
public class CommandHandler : Attribute
{
	public string Name { get; }

	public string Description { get; }

	public string[] Examples { get; }

	public CommandType CmdType { get; }

	public CommandHandler(string name, string description, CommandType type = CommandType.Player, params string[] examples)
	{
		Name = name;
		Description = description;
		CmdType = type;
		Examples = examples;
	}
}
