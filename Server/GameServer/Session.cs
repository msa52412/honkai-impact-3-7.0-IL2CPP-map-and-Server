using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Resources.Proto;
using Common.Utils;
using PemukulPaku.GameServer.Commands;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer;

public class Session
{
	public readonly string Id;

	public readonly TcpClient Client;

	public readonly Logger c;

	public long LastClientKeepAlive = Global.GetUnixInSeconds();

	public long LastServerKeepAlive = Global.GetUnixInSeconds();

	public Player Player = null;

	/// <summary>0 = 还连着，1 = 已经走过一次 DisconnectProtocol。</summary>
	private int _disconnected;

	public Session(string id, TcpClient client)
	{
		Id = id;
		Client = client;
		c = new Logger(Id);
		Task.Run((Action)ClientLoop);
		new Thread(KeepAliveLoop).Start();
	}

	private void ClientLoop()
	{
		NetworkStream stream = Client.GetStream();
		byte[] array = new byte[65536];
		while (Client.Connected)
		{
			try
			{
				Array.Clear(array, 0, array.Length);
				int num = stream.Read(array, 0, array.Length);
				if (num <= 0)
				{
					continue;
				}
				List<byte[]> list = new List<byte[]>();
				ReadOnlySpan<byte> value = new byte[4] { 1, 35, 69, 103 };
				ReadOnlySpan<byte> value2 = new byte[4] { 137, 171, 205, 239 };
				Span<byte> span = array.AsSpan();
				int num4;
				for (int i = 0; i < span.Length; i += num4)
				{
					int num2 = i;
					Span<byte> span2 = span.Slice(num2, span.Length - num2);
					int num3 = span2.IndexOf(value);
					if (num3 == -1)
					{
						break;
					}
					num4 = span2.IndexOf(value2);
					if (num4 == -1)
					{
						break;
					}
					num4 += value2.Length;
					num2 = num3;
					list.Add(span2.Slice(num2, num4 - num2).ToArray());
				}
				foreach (byte[] item in list)
				{
					if (Packet.IsValid(item))
					{
						ProcessPacket(new Packet(item), log: true);
						continue;
					}
					c.Error("Invalid packet received:", BitConverter.ToString(item).Replace("-", ""));
				}
			}
			catch (Exception)
			{
				break;
			}
		}
		DisconnectProtocol();
	}

	public void KeepAliveLoop()
	{
		while (Client.Connected)
		{
			try
			{
				Send(Packet.FromProto(new KeepAliveNotify(), CmdId.KeepAliveNotify));
				LastServerKeepAlive = Global.GetUnixInSeconds();
			}
			catch
			{
				break;
			}
			Thread.Sleep(3000);
		}
		DisconnectProtocol();
	}

	public void DisconnectProtocol()
	{
		// ClientLoop 和 KeepAliveLoop 都会走到这里，先抢一次，保证只执行一遍
		// （否则 Player.SaveAll() 会被调用两次，而且第二次 Remove 已经没意义）。
		if (Interlocked.Exchange(ref _disconnected, 1) != 0)
		{
			return;
		}

		try
		{
			// ★ Player 只有在登录成功（走完 mdk login + 16100 握手）之后才有值。
			//   只做 TCP 探活、或者客户端在握手前就断开时它是 null，
			//   原来这里直接 Player.SaveAll() 会抛 NullReferenceException；
			//   而抛异常的是后台线程 —— .NET 会直接终止整个进程。
			//   表现就是：验证脚本探一下 16100，服务端进程凭空消失。
			if (Player != null)
			{
				Player.SaveAll();
				c.Debug("Player data saved to database");
			}

			// 只摘掉「自己」：同一个 ip:port 重连时新 Session 会覆盖旧条目，
			// 旧 Session 晚到的 Disconnect 不能把新连接踢下线。
			if (Server.GetInstance().Sessions.TryRemove(new KeyValuePair<string, Session>(Id, this)))
			{
				Server.GetInstance().LogClients();
			}

			c.Warn(Id + " disconnected");

			if (ReadLine.GetInstance().session == this)
			{
				ReadLine.GetInstance().session = null;
			}
		}
		catch (Exception e)
		{
			c.Error("DisconnectProtocol 出错（已忽略，不影响其它连接）: " + e.Message);
		}
	}

	public void ProcessPacket(Packet _packet, bool log = false)
	{
		string name = Enum.GetName(typeof(CmdId), _packet.CmdId);
		if (name == "KeepAliveNotify")
		{
			LastClientKeepAlive = Global.GetUnixInSeconds();
			c.Log(name);
			return;
		}
		try
		{
			CmdId cmdId = (CmdId)Enum.ToObject(typeof(CmdId), _packet.CmdId);
			IPacketHandler packetHandler = PacketFactory.GetPacketHandler(cmdId);
			if (packetHandler == null)
			{
				c.Warn(name + " not handled!");
				return;
			}
			if (log)
			{
				c.Log(name);
			}
			packetHandler.Handle(this, _packet);
		}
		catch (Exception ex)
		{
			if (Global.config.VerboseLevel > VerboseLevel.Silent)
			{
				c.Error(ex.Message);
			}
		}
	}

	public void Send(params Packet[] packets)
	{
		foreach (Packet packet in packets)
		{
			string name = Enum.GetName(typeof(CmdId), packet.CmdId);
			try
			{
				Client.GetStream().Write(packet.Raw, 0, packet.Raw.Length);
				c.Log(name);
			}
			catch (ObjectDisposedException)
			{
				DisconnectProtocol();
			}
			catch (Exception ex2)
			{
				c.Error("Failed to send " + name + ":" + ex2.Message);
			}
		}
	}

	public override string ToString()
	{
		return Id;
	}

	public static Session? FromUid(uint uid)
	{
		return Server.GetInstance().Sessions.FirstOrDefault<KeyValuePair<string, Session>>((KeyValuePair<string, Session> s) => s.Value.Player.User.Uid == uid)!.Value;
	}
}
