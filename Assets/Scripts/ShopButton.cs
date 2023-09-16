using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

//helper class to make easier to edit shop buttons
public class ShopButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText,priceText;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Button buyButton;
    public string pieceName{set=>nameText.text=value;}
    public string piecePrice{set=>priceText.text=value;}
    public Sprite icon{set=>iconImage.sprite=value;}
    public bool interactable{set=>buyButton.interactable=value;}
    public UnityAction onClick{set=>buyButton.onClick.AddListener(value);}
    public Vector2 size=>rectTransform.rect.size;
    public Vector2 position{set=>rectTransform.localPosition=value;}
#if UNITY_EDITOR //this block will only compile in the editor
    void OnValidate()
    {
        rectTransform=GetComponent<RectTransform>();
    }
#endif
}
