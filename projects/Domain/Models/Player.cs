using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Keeper.Domain.Core;

namespace Keeper.Domain.Models
{
	public class Player : Entity, IAggregateRoot
	{
		public string Nickname { get; private set; }
		public string Name { get; private set; }
		private Player() { }

		public Player(string name)
		{
			Name = name;
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(Nickname) ? Name : Nickname;
		}
	}
}
