using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInteract : MonoBehaviour
{
    PlayerInventory inventory;
    public int numSlot;
    
    
    //public SistemaVida ManejoVida;
    //private int vida = 25; //Vida que recuperá al usar el objeto


    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerInventory.instance;
    }

    public void UseItem() //Que  sucede si apreta el boton (agregar acciones depende del objeto), agregar mas opciones si hay mas.
    {
        Debug.Log("Aqui hay" + inventory.items[numSlot].name);

        //Comida

        if(inventory.items[numSlot].type == ItemType.comida && inventory.items[numSlot].cantidad == 1)
        {
            inventory.EmptyInvent(numSlot, GetComponent<UnityEngine.UI.Image>(), ItemType.comida);
            //ManejoVida.Recovery(vida);
            //AudioManager.instance.PlaySFX("");
        }

        if (inventory.items[numSlot].type == ItemType.comida && inventory.items[numSlot].cantidad > 1)
        {
            inventory.items[numSlot].cantidad -= 1;
            //ManejoVida.Recovery(vida);
            //AudioManager.instance.PlaySFX("");
        }

        //Pluma
        if (inventory.items[numSlot].type == ItemType.pluma && inventory.items[numSlot].cantidad == 1)
        {
            inventory.EmptyInvent(numSlot, GetComponent<UnityEngine.UI.Image>(), ItemType.pluma);
            //AudioManager.instance.PlaySFX("");
        }

        if (inventory.items[numSlot].type == ItemType.pluma && inventory.items[numSlot].cantidad > 1)
        {
            inventory.items[numSlot].cantidad -= 1;
            //AudioManager.instance.PlaySFX("");
        }

        //Midnight
        if (inventory.items[numSlot].type == ItemType.midnight && inventory.items[numSlot].cantidad == 1)
        {
            inventory.EmptyInvent(numSlot, GetComponent<UnityEngine.UI.Image>(), ItemType.midnight);
        }

        if (inventory.items[numSlot].type == ItemType.midnight && inventory.items[numSlot].cantidad > 1)
        {
            inventory.items[numSlot].cantidad -= 1;
        }

        //Fragmentos
        if (inventory.items[numSlot].type == ItemType.fragmentos && inventory.items[numSlot].cantidad == 1)
        {
            inventory.EmptyInvent(numSlot, GetComponent<UnityEngine.UI.Image>(), ItemType.fragmentos);
        }

        if (inventory.items[numSlot].type == ItemType.fragmentos && inventory.items[numSlot].cantidad > 1)
        {
            inventory.items[numSlot].cantidad -= 1;
        }

    }
}
