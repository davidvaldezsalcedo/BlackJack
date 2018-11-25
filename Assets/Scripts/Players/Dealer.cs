using System.Collections;
using UnityEngine;

public class Dealer : Hand 
{
    [SerializeField]
    protected GameObject _CardPrefab;

    protected override void OnEnable()
    {
        base.OnEnable();
        HandEvents.PlayerStandEvent += OnTurnBegin;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        HandEvents.PlayerStandEvent -= OnTurnBegin;
    }

    protected override void InitHand()
    {
        bool showCardFace = false;

        if(_LoadedGame.Value == true)
        {
            for (int i = 0; i < _MyHand.Value.Count; i++)
            {
                if(i > 0)
                {
                    showCardFace = true;
                }
                Vector2 cardPosition = new Vector2(transform.position.x + i, transform.position.y);
                
                _CardsInPlay.Value.Add(_MyHand.Value[i], _CardPrefab);
                HandEvents.Trigger_AddCardEvent(cardPosition, _MyHand.Value[i], showCardFace);
            }
            return;
        }

        base.InitHand();

        for (int i = 0; i < 2; ++i)
        {
            if(i > 0)
            {
                showCardFace = true;
            }
            Hit(showCardFace);
        }
    }

    private void OnTurnBegin()
    {
        StartCoroutine(CheckCards());
    }

    private IEnumerator CheckCards()
    {
        foreach (var item in _CardsInPlay.Value)
        {
            if(item.Key == _MyHand.Value[0])
            {
                if(_LoadedGame.Value == false)
                {
                    Cards card = item.Value.GetComponent<Cards>();
                    card.ShowCardSide(card.CardFaces[item.Key]);
                }
                else
                {
                    GameObject newCard = Instantiate(_CardPrefab);
                    newCard.transform.position = transform.position;

                    Cards card = newCard.GetComponent<Cards>();
                    card.CardValue = item.Key;
                    card.ShowCardSide(card.CardFaces[item.Key]);
                }
            }
        }

        while(_MyHandValue.Value < 17)
        {
            yield return new WaitForSeconds(1);
            Hit(true);
        }

        if (CheckIfBusted(_MyHandValue.Value) != true)
        {
            HandEvents.Trigger_DealerStandEvent();
        }
        else
        {
            HandEvents.Trigger_IsBustedEvent("Dealer", "Player", false);
        }
    }
}
