using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialInstructionsManager : MonoBehaviour
{

    private static TutorialInstructionsManager _instance;
    public static TutorialInstructionsManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TutorialInstructionsManager>();
            }
            return _instance;
        }
    }

    public GameObject TextBox, TextBoxFinalPosition, timeobject;

    public TextMeshProUGUI TextBox_txt;
    Tweener SubsAni;
    Vector3 Pos;


    private void Start()
    {
        Pos = TextBox.transform.position;
        TriggerInstruction(0);
    }


    public void TriggerInstruction(int identifier)
    {
        if(identifier == 0)
        {
            TextBox_txt.text = "Usa WASD para moverte";
        }

        if (identifier == 1)
        {
            TextBox_txt.text = "Usa Espacio para saltar";
        }

        if (identifier == 2)
        {
            TextBox_txt.text = "Roier puede saltar dos veces para llegar a lugares mas altos";
        }
        if (identifier == 3)
        {
            TextBox_txt.text = "Roier puede empujar cajas presionando CLICK DERECHO";
        }
        if (identifier == 4)
        {
            TextBox_txt.text = "Puedes cambiar de personaje usando TAB";
        }
        if (identifier == 5)
        {
            TextBox_txt.text = "Cellbit, presionando CLICK CENTRAL del ratón, podrá interactuar con algunas cosas";
        }
        if (identifier == 6)
        {
            TextBox_txt.text = "Cellbit, presionando CLICK IZQUIERDO del ratón, lanzará una pluma";
        }
        if (identifier == 7)
        {
            TextBox_txt.text = "Puedes, interactuar presionando E";
        }
        if (identifier == 8)
        {
            TextBox_txt.text = "Quizá una luz sea necesaria para avanzar por aquí";
        }
        if (identifier == 9)
        {
            TextBox_txt.text = "Cellbit, presionando Q puede encenderla";
        }



        
        
            timeobject.transform.position = Pos;
            
            SubsAni = TextBox.transform.DOMove(TextBoxFinalPosition.transform.position, 3);
            SubsAni.OnComplete(() => {
                timeobject.transform.DOMove(TextBoxFinalPosition.transform.position, 4).OnComplete(() => {
                    TextBox.transform.DOMove(Pos, 3);
                    Debug.Log("asdf");
                    
                }); ;


            });

        
        
        
        

    }

    


}
