using System;
using System.Collections.Generic;
using System.Linq;

namespace PemukulPaku.GameServer.Game.Chatrooms;

public abstract class BaseChatroom<TSelf> where TSelf : BaseChatroom<TSelf>
{
	public readonly Dictionary<uint, Chatroom> Chatrooms = new Dictionary<uint, Chatroom>();

	private static TSelf? Instance;

	public static TSelf GetInstance()
	{
		return Instance ?? (Instance = Activator.CreateInstance<TSelf>());
	}

	public virtual Chatroom Join(in Session session, uint Id)
	{
		Chatrooms.TryGetValue(Id, out var value);
		if (value != null)
		{
			value.Join(in session);
			return value;
		}
		Chatrooms.Add(Id, new Chatroom(Id));
		return Join(in session, Id);
	}

	public virtual void Left(Session session)
	{
		Session session2 = session;
		Chatroom chatroom = Chatrooms.Values.Where((Chatroom chatr) => chatr.Members.Contains(session2)).FirstOrDefault();
		if (chatroom != null)
		{
			chatroom.Members.Remove(session2);
			if (chatroom.Members.Count < 1)
			{
				Chatrooms.Remove(chatroom.Id);
			}
		}
	}

	public virtual Chatroom GetChatroom(Session session)
	{
		Session session2 = session;
		Chatroom chatroom = Chatrooms.Values.Where((Chatroom chatr) => chatr.Members.Contains(session2)).FirstOrDefault();
		return chatroom ?? Join(in session2, 1u);
	}
}
