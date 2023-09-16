using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customization;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed=3;
    [SerializeField]
    Animator characterAnimator,clothAnimator,hairAnimator,hatAnimator;//used separated Animators inteady of an array to be clear which is which
    // Animator[] animators;
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
    }
    private void OnTriggerEnter(Collider col) {
        IInteractable i=col.GetComponent<IInteractable>();
        if(i!=null){
            interactables.Add(i);
        }
    }
    private void OnTriggerExit(Collider col) {
        IInteractable i=col.GetComponent<IInteractable>();
        if(i!=null){
            interactables.Remove(i);
            if(i==interactable)interactable.DeInteract();
        }
    }
#endregion
    public void EquipPiece(CustomType type,RuntimeAnimatorController pieceAnimator){
        switch (type)
        {
            case CustomType.Character:
                //no base character customization implemented
                break;
            case CustomType.Cloth:
                clothAnimator.runtimeAnimatorController=pieceAnimator;
                break;
            case CustomType.Hair:
                hairAnimator.runtimeAnimatorController=pieceAnimator;
                break;
            case CustomType.Hat:
                hatAnimator.runtimeAnimatorController=pieceAnimator;
                break;
        }
        //animator[(int)type].runtimeAnimatorController=pieceAnimator;
    }
    void Start()
    {
        
    }
    void Update()
    {
        Vector2 inputs=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        transform.Translate(inputs*movementSpeed*Time.deltaTime);
    }
}
