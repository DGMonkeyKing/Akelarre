using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string playerName = "player";
    private float _life = 1f;
    
    private List<Card> listOfCards = new List<Card>();
    private float drawPosition = 0f;

    [SerializeField]
    private CardGrabber cardGrabber;


    public delegate void OnCardPlayed(Player player, Card card);
    public event OnCardPlayed onCardPlayed;
    
    private void OnEnable() {
    }

    private void OnDisable() {
        
    }

    public void PlayCard(Card card)
    {
        //card.PlayAnimation();
        listOfCards.Remove(card);
        OrderHand(card, false);

        //Reset value
        card.transform.SetParent(null);
        onCardPlayed(this, card);

        //card.onCardChoosen -= PlayCard;

        drawPosition += 0.35f;
    }

    public void DrawCard(Card card)
    {
        card.transform.SetParent(cardGrabber.transform);
        card.transform.localPosition = new Vector3(drawPosition, -1.5f, 4.43f);
        cardGrabber.DrawCard(card);
        //card.onCardChoosen += PlayCard;

        OrderHand(card, true);
        listOfCards.Add(card);
        //card.DrawAnimation();

        drawPosition -= 0.35f;
    }

    private void OrderHand(Card card, bool isDraw)
    {
        //Modificar el valor de la X para colocar las cartas y que sean visibles
        foreach(Card c in listOfCards)
        {
            //c.MoveAnimation(isDraw);
        }
    }

    public bool IsDead()
    {
        return _life <= 0f;
    }

    public void OnCardTarget()
    {
        // Me quedo con la maldición
        Debug.Log("Player " + playerName + " tiene la maldición.");
    }
}
