using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ToXPositionsFromHere : ICardEffects
{
    [SerializeField]
    [Tooltip("Negative numbers means left. Positive right.")]
    private int positions;

    public void CardEffect(Player player, List<Player> players)
    {
        Player target = players.FindAll(p => p.IsDead())[(players.IndexOf(player) + positions)];

        target.OnCardTarget();
    }
}
