using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInteract : MonoBehaviour
{
    PlayerInventory inventory;
    public int numSlot;
    

    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerInventory.instance;
    }

    public void UseItem() //Que  sucede si apreta el boton (agregar acciones depende del objeto), agregar mas opciones si hay mas.
    {
        Debug.Log("Aqui hay" + inventory.items[numSlot].name);

        //Coins

        if(inventory.items[numSlot].type == ItemType.coins && inventory.items[numSlot].cantidad == 1)
        {
            //inventory.EmptyInvent(numSlot, GetComponent<UnityEngine.UI.Image>(), ItemType.coins);
            //AudioManager.instance.PlaySFX("");
        }

        if (inventory.items[numSlot].type == ItemType.coins && inventory.items[numSlot].cantidad > 1)
        {
            //inventory.items[numSlot].cantidad -= 1;
            //ManejoVida.Recovery(vida);
            //AudioManager.instance.PlaySFX("");
        }

        //Linterna
        if (inventory.items[numSlot].type == ItemType.linterna && inventory.items[numSlot].cantidad == 1)
        {
            //inventory.EmptyInvent(numSlot, GetComponent<UnityEngine.UI.Image>(), ItemType.linterna);
            //AudioManager.instance.PlaySFX("");
        }

        if (inventory.items[numSlot].type == ItemType.linterna && inventory.items[numSlot].cantidad > 1)
        {
            //inventory.items[numSlot].cantidad -= 1;
            //AudioManager.instance.PlaySFX("");
        }

    }
}
