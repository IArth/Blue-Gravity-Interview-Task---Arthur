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
    private Vector3 shopPanelScale;
    public Vector3 position => transform.position;
    public bool imediate=>false;
    public void OnInteract()
    {
        if(shopPanel.localScale!=Vector3.zero)return;
        shopPanelScale=Vector3.one;
        BuildMenu();
    }
    public void DeInteract()
    {
        if(shopPanel.localScale!=Vector3.one)return;
        shopPanelScale=Vector3.zero;
    }
    private void BuildMenu(){
        CustomPiece[] pieces=scriptable.GetPieces();
        buttomPrefab.gameObject.SetActive(true);
        Vector2 size=buttomPrefab.size;
        int count=0;
        foreach (CustomPiece piece in pieces)
        {
            bool obtained=player.Obtained(piece.name),equiped=player.IsEquiped(piece.name);
            ShopButton button=Instantiate(buttomPrefab,shopPanel);
            button.interactable=!equiped && (obtained || piece.price<=player.coins);
            button.onClick=()=>BuyPiece(piece);
            button.pieceName=piece.name;
            button.piecePrice=obtained?(equiped?"Equiped":"Equip"):piece.price.ToString();
            button.icon=piece.icon;
            button.position=new Vector2(count%4*size.x,count/4*-size.y);
            count++;
            buttons.Add(button);
        }
        buttomPrefab.gameObject.SetActive(false);
    }
    private void BuyPiece(CustomPiece piece){
        if(!player.Obtained(piece.name)){
            player.Obtain(piece.name);
            player.Spend(piece.price);
            Debug.Log("Buying "+piece.name);
        }
        Debug.Log("Equiping "+piece.name);
        player.EquipPiece(piece.pieceType,piece.animatorController,piece.name);
        RefreshPrices();
    }
    private void RefreshPrices(){
        CustomPiece[] pieces=scriptable.GetPieces();
        for (int i = 0; i < pieces.Length; i++)
        {
            bool obtained=player.Obtained(pieces[i].name),equiped=player.IsEquiped(pieces[i].name);
            buttons[i].interactable=!equiped && (obtained || pieces[i].price<=player.coins);
            buttons[i].pieceName=pieces[i].name;
            buttons[i].piecePrice=obtained?(equiped?"Equiped":"Equip"):pieces[i].price.ToString();
        }
    }
    void Start()
    {
        // BuildMenu();
    }
    void Update()
    {
        shopPanel.localScale=Vector3.MoveTowards(shopPanel.localScale,shopPanelScale,Time.deltaTime/.1f);
        if(shopPanel.localScale==Vector3.zero){
            for (int i = 1; i < shopPanel.childCount; i++)
            {
                Destroy(shopPanel.GetChild(1).gameObject);
            }
            buttons.Clear();
        }
    }
}
