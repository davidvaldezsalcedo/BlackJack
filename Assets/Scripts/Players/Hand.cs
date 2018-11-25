using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class Hand : MonoBehaviour 
{
    [SerializeField]
    private ListVariable _CardsInDeckList;
    [SerializeField]
    protected ListVariable _MyHand;
    [SerializeField]
    protected DictVariable _CardsInPlay;
	[SerializeField]
	protected IntVariable _MyHandValue;
    [SerializeField]
    protected BoolVariable _LoadedGame;

    protected virtual void OnEnable()
    {
        HandEvents.SaveGameEvent += SaveData;
        HandEvents.PlayersInitHandEvent += InitHand;
    }

    protected virtual void OnDisable()
    {
        HandEvents.SaveGameEvent -= SaveData;
        HandEvents.PlayersInitHandEvent -= InitHand;
    }

    protected virtual void InitHand() 
    { 
        _MyHand.Value = new List<int>();
        _MyHandValue.Value = 0;
    }

    public virtual void Hit(bool showCardFace)
    {
        _MyHand.Value.Add(_CardsInDeckList.Value[_CardsInDeckList.Value.Count - 1]);
        Vector2 cardPosition = new Vector2(transform.position.x + _MyHand.Value.Count, transform.position.y);
        HandEvents.Trigger_AddCardEvent(cardPosition, _MyHand.Value[_MyHand.Value.Count - 1], showCardFace);
        HandEvents.Trigger_CardTakenFromDeckEvent();    
        HandTotal();
    }

    protected int HandTotal()
    {
        _MyHandValue.Value = 0;
        int acesInHand = 0;

        foreach (var card in _MyHand.Value)
        {
            int cardValue = card % 13;

            if(cardValue > 0 && cardValue < 9)
            {
                cardValue += 1;
            }
            else if(cardValue >= 9 && cardValue < 13)
            {
                cardValue = 10;
            }
            else
            {
                acesInHand++;
            }

            _MyHandValue.Value += cardValue;
        }

        for (int i = 0; i < acesInHand; ++i)
        {
            if((_MyHandValue.Value + 11) <= 21)
            {
                _MyHandValue.Value += 11;
            }
            else
            {
                _MyHandValue.Value += 1;
            }
        }

        return _MyHandValue.Value;
    }
   
	protected bool CheckIfBusted(int handValue)
	{
		return (handValue > 21) ? true : false;
	}

    protected void SaveData()
    {
        HandEvents.Trigger_SaveGameEvent();
    }
}
