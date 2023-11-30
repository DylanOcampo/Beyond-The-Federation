using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    //Los tipos de items
    comida,
    pluma,
    midnight,
    fragmentos,
}
public class PlayerInventory : MonoBehaviour
{

    private bool inventoryActivado;
    public GameObject inventario;
    public static PlayerInventory instance;
    public Item[] items;

    private void Awake()
    {
        instance = this;
    }

    public void EmptyInvent(int numSlot, Image img, ItemType item)
    {
        items[numSlot].isFull = false;
        items[numSlot].cantidad = 0;
        items[numSlot].type = item;
        img.sprite = null;
        img.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryActivado)
            {
                Desactivado();
            }
            else
            {
                Activado();
            }
        }
    }

    public void Activado()
    {
        inventario.SetActive(true);
        inventoryActivado = true;
    }

    public void Desactivado()
    {
        inventario.SetActive(false);
        inventoryActivado = false;
    }
}


[System.Serializable] //Guardar
public class Item //Todas las caracteristicas del objeto
{
    public bool isFull;
    public int cantidad;
    public ItemType type;
    public string name;
    public GameObject slotSprite;
}
