using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_persist : MonoBehaviour
{
    
    void Awake()
    {
        int numGamePersist = FindObjectsOfType<Scene_persist >().Length;
        if(numGamePersist>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public  void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
