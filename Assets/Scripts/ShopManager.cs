using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Customization;

public class ShopManager : MonoBehaviour,IInteractable
{
    [SerializeField]
    private RectTransform buttomPrefab;
    [SerializeField]
    private CustomizationScriptable scriptable;
    [SerializeField]
    private RectTransform shopPanel;
    [SerializeField]
    private Player player;
    private Vector3 shopPanelScale;
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
        foreach (CustomPiece piece in pieces)
        {
            RectTransform buttonTransform=Instantiate(buttomPrefab);
            Button button=buttonTransform.GetComponent<Button>();
            button.onClick.AddListener(()=>BuyPiece(piece));
            button.interactable=piece.price<100;
            Text[] texts=buttonTransform.GetComponentsInChildren<Text>();
            texts[0].text=piece.name;
            texts[1].text=piece.price.ToString();
            buttonTransform.GetComponentInChildren<Image>().sprite=piece.icon;
        }
    }
    private void BuyPiece(CustomPiece piece){
        Debug.Log("Buying "+piece.name);
        player.EquipPiece(piece.pieceType,piece.animatorController);
    }
    void Start()
    {
    }
    void Update()
    {
    }
}
