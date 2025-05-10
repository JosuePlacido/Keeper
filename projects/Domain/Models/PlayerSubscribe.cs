using System;
using System.Linq;
using Domain.Core;
using Domain.Enum;

namespace Domain.Models;
public class PlayerSubscribe : Entity
{
	public string ChampionshipId { get; private set; }
	public string TeamSubscribeId { get; private set; }
	public string PlayerId { get; private set; }
	public Player Player { get; private set; }
	public int Games { get; private set; }
	public int Goals { get; private set; }
	public int YellowCard { get; private set; }
	public int RedCard { get; private set; }
	public int MVPs { get; private set; }
	public bool IsFreeAgent { get; private set; }

	public PlayerSubscribe() { }
	public PlayerSubscribe(string player)
		: base(Guid.NewGuid().ToString())
	{
		PlayerId = player;
		IsFreeAgent = true;
	}
	public PlayerSubscribe(string player, string team)
				: base(Guid.NewGuid().ToString())
	{
		PlayerId = player;
		TeamSubscribeId = team;
		IsFreeAgent = true;
	}
	public override string ToString()
	{
		return (Player == null) ? base.ToString() : $"{Player.ToString()}";
	}
	public static PlayerSubscribe Factory(string id, string playerId, string championshipId,
		string teamSubscribeId = null, Player player = null, int games = 0,
		int goals = 0, int yellowCard = 0, int redCard = 0,
		int mVPs = 0)
	{
		return new PlayerSubscribe()
		{
			Id = id,
			ChampionshipId = championshipId,
			TeamSubscribeId = teamSubscribeId,
			PlayerId = playerId,
			Player = player,
			Games = games,
			Goals = goals,
			YellowCard = yellowCard,
			RedCard = redCard,
			MVPs = mVPs,
			IsFreeAgent = teamSubscribeId == null
		};
	}

	public void TransferTeam(string teamSubscribeId)
	{
		TeamSubscribeId = teamSubscribeId;
		IsFreeAgent = false;
	}

	public void UpdateNumbers(int? games = null, int? goals = null, int? yellowCard = null,
		int? redCard = null, int? mVPs = null)
	{
		if (games != null)
			Games = (int)games;
		if (goals != null)
			Goals = (int)goals;
		if (yellowCard != null)
			YellowCard = (int)yellowCard;
		if (redCard != null)
			RedCard = (int)redCard;
		if (mVPs != null)
			MVPs = (int)mVPs;
	}

	internal void RegisterEvents(EventGame[] events)
	{
		Games++;
		Goals += events.Where(ev => ev.Type == TypeEvent.Goal).Count();
		MVPs += events.Where(ev => ev.Type == TypeEvent.MVP).Count();
		YellowCard += events.Where(ev => ev.Type == TypeEvent.YellowCard).Count();
		RedCard += events.Where(ev => ev.Type == TypeEvent.RedCard).Count();
	}

	internal void UpdateResult(int goalsAux, int yellowsAux,
		int redsAux, int mvpsAux, bool isUpdate)
	{
		if (!isUpdate)
		{
			Games++;
		}
		Goals += goalsAux;
		YellowCard += yellowsAux;
		RedCard += redsAux;
		MVPs += mvpsAux;
	}
}
