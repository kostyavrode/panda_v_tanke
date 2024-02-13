using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        GameObject[] trash = GameObject.FindGameObjectsWithTag("Blood");
        foreach(GameObject obj in trash)
        {
            Destroy(obj);
        }
    }
}
