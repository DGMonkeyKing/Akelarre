using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private ObjectPool OP;

    private int numberOfCard;
    [SerializeField]
    private GameObject cardPrefab;

    void Awake()
    {
        numberOfCard = PlayerPrefs.GetInt(GameData.NUMBER_OF_CARDS, 120);
        OP = new ObjectPool(cardPrefab, numberOfCard, gameObject);

        ConfigureListOfCardsOfAkelarre();
    }

    private void ConfigureListOfCardsOfAkelarre()
    {
        //En esta función se va a configurar las cartas, para ver que cartas se ponen en el mazo o no.

    }

    public Card DrawCard()
    {
        return OP.GetObject().GetComponent<Card>();
    }

    public void DiscardCard(Card card)
    {
        OP.ReturnObject(card.gameObject);
    }
}
