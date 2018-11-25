using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour 
{
	[SerializeField]
	private IntVariable _DealerHandValue;
	[SerializeField]
	private IntVariable _PlayerHandValue;
    [SerializeField]
	private BoolVariable _LoadedGame;
	[SerializeField]
	private int _StartingChips = 200;
	[SerializeField, Header("UI")]
	private GameObject WinnerUI;
	[SerializeField]
	private Button[] _ButtonsToDisable;
	[SerializeField]
	private Text _CurrentBetText;
	[SerializeField]
	private Text _PlayerChipsLeftText;
    [SerializeField]
    private IntVariable _CurrentBet;
    [SerializeField]
	private IntVariable _PlayerChipsLeft;

	private void OnEnable()
	{
		HandEvents.DealerStandEvent += CompareHands;
		HandEvents.IsBustedEvent += Busted;
		HandEvents.PlayerAddToCurrentBetEvent += AddToCurrentBet;
	}

	private void OnDisable()
	{
		HandEvents.DealerStandEvent -= CompareHands;
		HandEvents.IsBustedEvent -= Busted;
		HandEvents.PlayerAddToCurrentBetEvent -= AddToCurrentBet;
	}

	private void Start()
	{
		if(_LoadedGame.Value == false)
		{
			_PlayerChipsLeft.Value = _StartingChips;
			_CurrentBet.Value = 0;
		}

		_PlayerChipsLeftText.text = "Chips Left - $" + _PlayerChipsLeft.Value;
		_CurrentBetText.text = "Current Bet - $" + _CurrentBet.Value;
	}

	private void CompareHands()
    {
        Text winnerText = WinnerUI.GetComponentInChildren<Text>();
        if (_DealerHandValue.Value > _PlayerHandValue.Value)
        {
            winnerText.text = "Dealer Wins!!";
            _PlayerChipsLeft.Value = SettleBets(false);
        }
        else if (_DealerHandValue.Value < _PlayerHandValue.Value)
        {
            winnerText.text = "Player Wins!!";
            _PlayerChipsLeft.Value = SettleBets(true);
        }
        else
        {
            winnerText.text = "It's a Tie!!";
            _PlayerChipsLeft.Value += _CurrentBet.Value;
        }
        SetEndGame();
    }

    private void Busted(string bustedPlayer, string winnerPlayer, bool playerBusted)
	{
		Text winnerText = WinnerUI.GetComponentInChildren<Text>();
		SettleBets(playerBusted);
		_PlayerChipsLeftText.text = "Chips Left - $" + _PlayerChipsLeft.Value;
		winnerText.text = bustedPlayer + " Is Busted! " + winnerPlayer + " Wins!!";
		SetEndGame();
	}

    private void SetEndGame()
    {
        for (int i = 0; i < _ButtonsToDisable.Length; i++)
        {
            _ButtonsToDisable[i].interactable = false;
        }
        _PlayerChipsLeftText.text = "Chips Left - $" + _PlayerChipsLeft.Value;
        WinnerUI.SetActive(true);
    }

	private void AddToCurrentBet(int amountToAdd)
	{
        if(amountToAdd > _PlayerChipsLeft.Value)
        {
            _CurrentBetText.text = "Current Bet - $" + _CurrentBet.Value + "\n" + "Not Enough Chips!!";
            return;
        }

        _CurrentBet.Value += amountToAdd;
        _PlayerChipsLeft.Value -= amountToAdd;

		_CurrentBetText.text = "Current Bet - $" + _CurrentBet.Value;
		_PlayerChipsLeftText.text = "Chips Left - $" + _PlayerChipsLeft.Value;
	}

	private int SettleBets(bool playerWon)
	{
		if(playerWon == true)
		{
			return _PlayerChipsLeft.Value += _CurrentBet.Value * 2;
		}

		return 0;
	}
}
