using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script made in previous game jam
public interface IInteractable
{
    bool imediate{get;}
    Vector3 position{get;}
    void OnInteract();
    void DeInteract();
}
