using System;
using System.Diagnostics;

namespace Common.Utils;

public class Logger
{
	private readonly string _name;

	private readonly bool TraceOnError;

	private readonly ConsoleColor _color;

	public Logger(string name, ConsoleColor color = ConsoleColor.Cyan, bool traceOnError = true)
	{
		_name = name;
		_color = color;
		TraceOnError = traceOnError;
	}

	public void Log(params string[] message)
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(DateTime.Now.ToString("HH:mm:ss "));
		Console.ResetColor();
		Console.Write("<");
		Console.ForegroundColor = _color;
		Console.Write(_name);
		Console.ResetColor();
		Console.Write("> ");
		Console.WriteLine(string.Join("\t", message));
		Console.ResetColor();
	}

	public void Warn(params string[] message)
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(DateTime.Now.ToString("HH:mm:ss "));
		Console.ResetColor();
		Console.Write("<");
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write(_name);
		Console.ResetColor();
		Console.Write("> ");
		Console.WriteLine(string.Join("\t", message));
		Console.ResetColor();
	}

	public void Trail(params string[] msg)
	{
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.WriteLine("\t└── " + string.Join(' ', msg));
		Console.ResetColor();
	}

	public void Error(params string[] message)
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(DateTime.Now.ToString("HH:mm:ss "));
		Console.ResetColor();
		Console.Write("<");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(_name);
		Console.ResetColor();
		Console.Write("> ");
		Console.ForegroundColor = ConsoleColor.White;
		if (TraceOnError)
		{
			Console.BackgroundColor = ConsoleColor.DarkRed;
		}
		Console.WriteLine(string.Join("\t", message));
		Console.ResetColor();
		StackTrace stackTrace = new StackTrace(fNeedFileInfo: true);
		if (TraceOnError)
		{
			Trail(stackTrace.ToString());
		}
	}

	public void Debug(params string[] message)
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(DateTime.Now.ToString("HH:mm:ss "));
		Console.ResetColor();
		Console.Write("<");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(_name);
		Console.ResetColor();
		Console.Write("> ");
		Console.ForegroundColor = ConsoleColor.White;
		Console.BackgroundColor = ConsoleColor.DarkMagenta;
		Console.WriteLine(string.Join("\t", message));
		Console.ResetColor();
	}
}
