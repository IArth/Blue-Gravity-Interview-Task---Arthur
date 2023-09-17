using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Customization;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed=3;
    [SerializeField]
    private string equipedClothes,equipedHair,equipedHat;
    [SerializeField]
    private GameObject interactionIndicator;
    [SerializeField]
    protected Animator characterAnimator,clothAnimator,hairAnimator,hatAnimator;//used separated Animators inteady of an array to be clear which is which
    [SerializeField]
    private Text coinsUI;
    // Animator[] animators;
    public int coins{private set;get;}=1000;
    public float _coins;
    private HashSet<string> obtainedPieces=new HashSet<string>();
#region Interactions - Made beforehand
    //interactable script made previously in a game jam
    List<IInteractable> interactables=new List<IInteractable>();
    IInteractable interactable;
    public void Interact(){
        if(interactable!=null){
            interactable.DeInteract();
            interactable=null;
            return;
        }
        if(interactables.Count==0)return;
        interactable=interactables[0];
        float distance=Vector3.Distance(transform.position,interactables[0].position);
        for (int i = 0; i < interactables.Count; i++)
        {
            float d=Vector3.Distance(transform.position,interactables[i].position);
            if(distance>d){
                distance=d;
                interactable=interactables[i];
            }
        }
        interactable.OnInteract();
        if(interactable.imediate)interactable=null;
    }
    private void OnTriggerEnter2D(Collider2D col) {
        IInteractable i=col.GetComponent<IInteractable>();
        if(i!=null){
            interactables.Add(i);
        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        IInteractable i=col.GetComponent<IInteractable>();
        if(i!=null){
            interactables.Remove(i);
            if(i==interactable)interactable.DeInteract();
        }
    }
#endregion
    public void EquipPiece(CustomType type,RuntimeAnimatorController pieceAnimator,string pieceName){
        float dir=characterAnimator.GetFloat("Direction");
        switch (type)
        {
            case CustomType.Character:
                //no base character customization implemented
                break;
            case CustomType.Cloth:
                clothAnimator.runtimeAnimatorController=pieceAnimator;
                clothAnimator.SetFloat("Direction",dir);
                equipedClothes=pieceName;
                break;
            case CustomType.Hair:
                hairAnimator.runtimeAnimatorController=pieceAnimator;
                hairAnimator.SetFloat("Direction",dir);
                equipedHair=pieceName;
                break;
            case CustomType.Hat:
                hatAnimator.runtimeAnimatorController=pieceAnimator;
                hatAnimator.SetFloat("Direction",dir);
                equipedHat=pieceName;
                break;
        }
        //animator[(int)type].runtimeAnimatorController=pieceAnimator;
    }
    public void GiveCoins(int amount)=>coins+=coins;
    public void Obtain(string pieceName)=> obtainedPieces.Add(pieceName);
    public bool Obtained(string pieceName)=>obtainedPieces.Contains(pieceName);
    public bool IsEquiped(string pieceName)=>equipedClothes==pieceName || equipedHair==pieceName || equipedHat==pieceName;
    public void Spend(int amount)=>coins-=Mathf.Abs(amount);
    void Animate(Vector2 input){
        int dir=0;
        if(input.x>0)dir=1;//east
        if(input.x<0)dir=4;//west
        if(input.y>0)dir=0;//north
        if(input.y<0)dir=2;//south
        bool walking=input!=Vector2.zero;
        
        Animate(characterAnimator,walking,dir);
        Animate(clothAnimator,walking,dir);
        Animate(hairAnimator,walking,dir);
        Animate(hatAnimator,walking,dir);

        //alternative script for animators array
        // for (int i = 0; i < animators.Length; i++)
        // {
        //     animators[i].SetBool("Walking",walking);
        //     if(walking)animators[i].SetFloat("Direction",dir);
        //     animators[i].SetFloat("Speed",movementSpeed);
        // }
    }
    void Animate(Animator animator,bool walking,int dir){
        if(!animator){
            Debug.LogError("Animator not assigned!",this);
            return;
        }
        animator.SetBool("Walking",walking);
        if(walking)animator.SetFloat("Direction",dir);
        animator.SetFloat("Speed",movementSpeed);
    }
    void Start()
    {
        obtainedPieces.Add(equipedClothes);
        obtainedPieces.Add(equipedHair);
        obtainedPieces.Add(equipedHat);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))Interact();
        interactionIndicator.SetActive(interactables.Count>0);
        Vector2 inputs=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        Animate(inputs);
        transform.Translate(inputs*movementSpeed*Time.deltaTime);
        //uses the Y axis of the player to move it on the Z axis to avoid layers ovelap with the shopkeeper
        Vector3 pos=transform.position;
        pos.z=Mathf.Floor(pos.y)/10;
        transform.position=pos;
        _coins=Mathf.MoveTowards(_coins,coins,Time.deltaTime*1000);
        coinsUI.text=_coins.ToString("0");
    }
}
