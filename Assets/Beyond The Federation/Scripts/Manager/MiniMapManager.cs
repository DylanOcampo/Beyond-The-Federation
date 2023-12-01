using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{

    public List<GameObject> MapLocations = new List<GameObject>();
    // Start is called before the first frame update

    public void ChangeTo(int i)
    {
        foreach (GameObject mapLocation in MapLocations)
        {
            mapLocation.gameObject.SetActive(false);
        }
        MapLocations[i].SetActive(true);
    }


    public void ChangeToFavela()
    {
        foreach (GameObject mapLocation in MapLocations)
        {
            mapLocation.gameObject.SetActive(false);
        }
        MapLocations[2].SetActive(true);
    }
    public void ChangeToLab()
    {
        foreach (GameObject mapLocation in MapLocations)
        {
            mapLocation.gameObject.SetActive(false);
        }
        MapLocations[3].SetActive(true);
    }
    public void ChangeToRoierHouse()
    {
        foreach (GameObject mapLocation in MapLocations)
        {
            mapLocation.gameObject.SetActive(false);
        }
        MapLocations[0].SetActive(true);
    }
    public void ChangeToCellbit()
    {
        foreach (GameObject mapLocation in MapLocations)
        {
            mapLocation.gameObject.SetActive(false);
        }
        MapLocations[4].SetActive(true);
    }
    public void ChangeToTutorial()
    {
        foreach (GameObject mapLocation in MapLocations)
        {
            mapLocation.gameObject.SetActive(false);
        }
        MapLocations[1].SetActive(true);
    }


}
