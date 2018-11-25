using UnityEngine;
using UnityEngine.UI;

public class Player : Hand 
{
    [SerializeField]
    private Button[] _Buttons;
    [SerializeField]
    private IntVariable _PlayerChipsLeft;
    [SerializeField]
    private GameObject _BettingUI;
    [SerializeField]
    private GameObject _MainGameButtons;

    protected override void OnEnable()
    {
        base.OnEnable();
        if(_LoadedGame.Value == true)
        {
            _BettingUI.SetActive(false);
            _MainGameButtons.SetActive(true);
            HandEvents.Trigger_PlayersInitEvent();
            
            return;
        }
    }

    protected override void InitHand()
    {
        if(_LoadedGame.Value == true)
        {
            for (int i = 0; i < _MyHand.Value.Count; i++)
            {
                Vector2 cardPosition = new Vector2(transform.position.x + i, transform.position.y);
                _CardsInPlay.Value.Add(_MyHand.Value[i], null);
                HandEvents.Trigger_AddCardEvent(cardPosition, _MyHand.Value[i], true);
            }
            return;
        }

        base.InitHand();

        for (int i = 0; i < 2; ++i)
        {
            Hit(true);
        }
    }

    public override void Hit(bool showCardFace)
    {
        base.Hit(showCardFace);

        if(CheckIfBusted(_MyHandValue.Value) == true)
        {
            DisableButtons();
            HandEvents.Trigger_IsBustedEvent("Player", "Dealer", true);
            return;
        }
    }

    public void Stand()
    {
        DisableButtons();
        HandEvents.Trigger_PlayerStandEvent();
    }

    private void DisableButtons()
    {
        for (int i = 0; i < _Buttons.Length; ++i)
        {
            _Buttons[i].interactable = false;
        }
    }

    public void AddToBet(int amount)
    {
        HandEvents.Trigger_PlayerAddToCurrentBetEvent(amount);
    }

    public void Bet()
    {
        HandEvents.Trigger_PlayersInitEvent();
    }
}
