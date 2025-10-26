using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMerchant : ObjectNPC, IInteractable
{
    public void Interact()
    {
        Debug.Log("Open shop...");
    }
}
