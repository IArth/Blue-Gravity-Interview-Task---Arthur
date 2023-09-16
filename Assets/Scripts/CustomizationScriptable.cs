using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Customization
{
    [CreateAssetMenu(fileName = "CustomizationScriptable", menuName = "Customization/CustomizationScriptable", order = 0)]
    public class CustomizationScriptable : ScriptableObject {
        [SerializeField]
        private CustomPiece[] pieces;
        public CustomPiece GetPiece(int i){
            if(i<0 || i>=pieces.Length){
                Debug.LogError("Outside of range:"+i);
                return new CustomPiece();
            }
            return pieces[i];
        }
        public CustomPiece[] GetPieces()=>pieces;
    }
    [System.Serializable]
    public struct CustomPiece
    {
        public string name;
        public int price;
        public Sprite icon;
        public RuntimeAnimatorController animatorController;
        public CustomType pieceType;
    }
    public enum CustomType
    {
        Character,Cloth,Hair,Hat
    }
}
