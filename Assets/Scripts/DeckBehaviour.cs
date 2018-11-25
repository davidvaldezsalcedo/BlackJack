using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour 
{
	[SerializeField]
	private ListVariable _CardsList;
	[SerializeField]
	private DictVariable _CardsTaken;
	[SerializeField]
	private DictVariable _CardsInPlay;
	[SerializeField]
	private BoolVariable _LoadedGame;
	
    [SerializeField]
    private Sprite _CardBacking;

	[SerializeField]
	private GameObject _CardPrefab;
	
	[SerializeField, Range(7, 10), Tooltip("Distance Deck Appears From center")]
	private float _DistanceOfCards = 10;

	private void Awake()
	{
		HandEvents.CardTakenFromDeckEvent += CardTakenFromDeck; 
		HandEvents.AddCardEvent += AddCardToStack; 

		_CardsTaken.Value = new Dictionary<int, GameObject>();
		_CardsInPlay.Value = new Dictionary<int, GameObject>();

		if(_LoadedGame.Value == true)
		{
			InstantiateDeck();
			return;
		}
		InitDeck();	
	}

	private void OnDestroy()
    {
        HandEvents.CardTakenFromDeckEvent -= CardTakenFromDeck;
		HandEvents.AddCardEvent -= AddCardToStack; 
    }

	private void InitDeck()
	{
		_CardsList.Value.Clear();

		_CardsList.Value = new List<int>(52);

		for (int i = 0; i < 52; ++i)
		{
			_CardsList.Value.Add(i);
		}

		ShuffleDeck();
		InstantiateDeck();
	}

	[ContextMenu("Shuffle")]
	private List<int> ShuffleDeck()
	{
		int count = _CardsList.Value.Count;
		for (int i = 0; i < count; ++i) 
		{
			int newIndex = UnityEngine.Random.Range(i, count);
			int tmpCard = _CardsList.Value[i];
			_CardsList.Value[i] = _CardsList.Value[newIndex];
			_CardsList.Value[newIndex] = tmpCard;
		}
		return _CardsList.Value;
	}

	private void InstantiateDeck()
	{
		float angle = 7;
		foreach (var cardIndex in _CardsList.Value)
        {
            angle += 7;
			Vector2 newPosition = CalculateAngle(angle, _DistanceOfCards, transform.position);
            AddCardToStack(newPosition, cardIndex, false);
        }
    }

    private void AddCardToStack(Vector2 newPosition, int cardIndex, bool showCardFace)
    {
        GameObject newCard = Instantiate(_CardPrefab);
        newCard.transform.position = newPosition;

        Cards card = newCard.GetComponent<Cards>();
        card.CardValue = cardIndex;
		
		if(showCardFace == true)
		{
			card.ShowCardSide(card.CardFaces[cardIndex]);
		}
		else
		{
			card.ShowCardSide(_CardBacking);
		}

		if(_CardsTaken.Value.ContainsKey(cardIndex) == false)
		{
			_CardsTaken.Value.Add(cardIndex, newCard);
		}
		else
		{
			_CardsInPlay.Value.Add(cardIndex, newCard);
		}
    }

    private void CardTakenFromDeck()
	{
		GameObject cardToRemove;
		int key = _CardsList.Value[_CardsList.Value.Count - 1];
		if(_CardsTaken.Value.TryGetValue(key, out cardToRemove))
		{
			Destroy(cardToRemove);
			_CardsTaken.Value.Remove(key);
		}
		_CardsList.Value.RemoveAt(_CardsList.Value.Count - 1);
	}

	private Vector2 CalculateAngle(float angle, float distance, Vector2 position)
	{
		float radian = angle * Mathf.Deg2Rad;
		float x = Mathf.Cos(radian);
		float z = Mathf.Sin(radian);
		Vector2 newPos = new Vector2 (x * distance, z * distance);
		newPos = position + newPos;
		return newPos;
	}
}
