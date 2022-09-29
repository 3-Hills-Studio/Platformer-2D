using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : BaseCollectable
{
    [SerializeField] private int amount;

    public override void Collected(Player player)
    {
        GameController.singleton.ModifyCoins(amount);
        Destroy(gameObject);
    }
}
