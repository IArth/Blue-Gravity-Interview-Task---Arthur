using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMaker : MonoBehaviour,IInteractable
{
    [SerializeField]
    private int coinAmount;
    [SerializeField]
    Player player;
    public bool imediate =>true;

    public Vector3 position => transform.position;

    public void DeInteract()
    {
        //this IInteractable is "imidiate", it only uses Interact and not DEInteract
    }

    public void OnInteract()
    {
        //this IInteractable is "imidiate", it only uses Interact and not DEInteract
        player.GiveCoins(coinAmount);
    }
    void Start()
    {
    }
    void Update()
    {
    }
}
