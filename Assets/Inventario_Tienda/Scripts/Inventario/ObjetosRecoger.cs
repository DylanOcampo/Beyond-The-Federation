using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjetosRecoger : MonoBehaviour
{
    PlayerInventory inventory;
    public ParticleSystem particle;
    public Sprite sprite;
    public ItemType type;
    public string nameItem;

    void Start()
    {
        inventory = PlayerInventory.instance;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            AudioManager.instance.PlayClip(8);
            for (int i = 0; i < inventory.items.Length; i++)
            {
                if(inventory.items[i].isFull == false)
                {
                    Debug.Log("Item añadido");
                    inventory.items[i].isFull = true;
                    inventory.items[i].cantidad = 1;
                    inventory.items[i].type = type;
                    inventory.items[i].name = nameItem;
                    inventory.items[i].slotSprite.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    inventory.items[i].slotSprite.GetComponent<UnityEngine.UI.Image>().enabled = true;
                    Destroy(gameObject);
                    if(particle != null)
                    {
                        Instantiate(particle, transform.position, Quaternion.identity);
                    }
                    
                    break;
                }

                if (inventory.items[i].isFull == true && inventory.items[i].name == nameItem && inventory.items[i].cantidad < 64)
                {
                    Debug.Log("Item estaqueado");
                    inventory.items[i].cantidad += 1;
                    Destroy(gameObject);
                    if(particle != null)
                    {
                        Instantiate(particle, transform.position, Quaternion.identity);
                    }
                    
                    break;
                }
            }
        }
    }
}
