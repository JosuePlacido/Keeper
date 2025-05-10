using Domain.Core;

namespace Domain.Models;
public class Player : Entity
{
	public string Nickname { get; private set; }
	public string Name { get; private set; }
	public string ImageUrl { get; private set; }
	private Player() { }

	public override string ToString()
	{
		return string.IsNullOrEmpty(Nickname) ? Name : Nickname;
	}
	public static Player Factory(string id, string name, string nick = null)
	{
		return new Player
		{
			Id = id,
			Name = name,
			Nickname = nick
		};
	}
}