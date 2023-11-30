using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvent //Para controlar lo que entra como evento para eso existe
{

    private object[] list;

    public AppEvent(params object[] list)
    {
        this.list = list;
    }

    public object[] GetParameters()
    {
        return list;
    }

}
