using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Customization;

public class ShopManager : MonoBehaviour,IInteractable
{
    [SerializeField]
    private ShopButton buttomPrefab;
    [SerializeField]
    private CustomizationScriptable scriptable;
    [SerializeField]
    private RectTransform shopPanel;
    [SerializeField]
    private Player player;
    private List<ShopButton> buttons=new List<ShopButton>();
    private Vector3 shopPanelScale=Vector3.one;
    public Vector3 position => transform.position;

    public void OnInteract()
    {
        shopPanelScale=Vector3.one;
        BuildMenu();
    }
    public void DeInteract()
    {
        shopPanelScale=Vector3.one;
    }
    private void BuildMenu(){
        CustomPiece[] pieces=scriptable.GetPieces();
        buttomPrefab.gameObject.SetActive(true);
        Vector2 size=buttomPrefab.size;
        int count=0;
        foreach (CustomPiece piece in pieces)
        {
            ShopButton button=Instantiate(buttomPrefab,shopPanel);
            button.onClick=()=>BuyPiece(piece);
            button.interactable=player.Obtained(piece.name) || piece.price<=player.coins;
            button.pieceName=piece.name;
            button.piecePrice=player.Obtained(piece.name)?"Equip":piece.price.ToString();
            button.icon=piece.icon;
            button.position=new Vector2(count%4*size.x,count/4*size.y);
            count++;
            buttons.Add(button);
        }
        buttomPrefab.gameObject.SetActive(false);
    }
    private void BuyPiece(CustomPiece piece){
        if(!player.Obtained(piece.name)){
            player.Obtain(piece.name);
            player.Spend(piece.price);
            RefreshPrices();
            Debug.Log("Buying "+piece.name);
        }
        Debug.Log("Equiping "+piece.name);
        player.EquipPiece(piece.pieceType,piece.animatorController);
    }
    private void RefreshPrices(){
        CustomPiece[] pieces=scriptable.GetPieces();
        for (int i = 0; i < pieces.Length; i++)
        {
            buttons[i].interactable=player.Obtained(pieces[i].name) || pieces[i].price<=player.coins;
            buttons[i].pieceName=pieces[i].name;
            buttons[i].piecePrice=player.Obtained(pieces[i].name)?"Equip":pieces[i].price.ToString();
        }
    }
    void Start()
    {
        BuildMenu();
    }
    void Update()
    {
        shopPanel.localScale=Vector3.MoveTowards(shopPanel.localScale,shopPanelScale,Time.deltaTime/1f);
    }
}
