using System;
using UnityEngine;

public static class HandEvents
{
	public static event Action CardTakenFromDeckEvent = delegate{};
	public static event Action<Vector2, int, bool> AddCardEvent = delegate{};
	public static event Action PlayerStandEvent = delegate{};
	public static event Action DealerStandEvent = delegate{};
	public static event Action<string, string, bool> IsBustedEvent = delegate{};
	public static event Action<int> PlayerAddToCurrentBetEvent = delegate{};
	public static event Action PlayersInitHandEvent = delegate{};
	public static event Action SaveGameEvent = delegate{};
	public static event Action LoadGameEvent = delegate{};

	public static void Trigger_CardTakenFromDeckEvent()
	{
		CardTakenFromDeckEvent.Invoke();
	}

	public static void Trigger_AddCardEvent(Vector2 newPosition, int cardIndex, bool showCardFace)
	{
		AddCardEvent.Invoke(newPosition, cardIndex, showCardFace);
	}

	public static void Trigger_PlayerStandEvent()
	{
		PlayerStandEvent.Invoke();
	}
	
	public static void Trigger_DealerStandEvent()
	{
		DealerStandEvent.Invoke();
	}

	public static void Trigger_IsBustedEvent(string whoIsBusted, string whoWins, bool playerBusted)
	{
		IsBustedEvent.Invoke(whoIsBusted, whoWins, playerBusted);
	}

	public static void Trigger_PlayerAddToCurrentBetEvent(int amountToAdd)
	{
		PlayerAddToCurrentBetEvent.Invoke(amountToAdd);
	}

	public static void Trigger_PlayersInitEvent()
	{
		PlayersInitHandEvent.Invoke();
	}

	public static void Trigger_SaveGameEvent()
	{
		SaveGameEvent.Invoke();
	}

	public static void Trigger_LoadGameEvent()
	{
		LoadGameEvent.Invoke();
	}
}
