using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VFXpool : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    List<GameObject> explosionList = new List<GameObject>();

    public static VFXpool instance;
    private void Awake()
    {
        instance = this;
        Spawm();
    }

    void Spawm()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject deadVFX = Instantiate(explosionVFX,this.transform);
            explosionList.Add(deadVFX);
            deadVFX.SetActive(false);
        }
    }

    public void PlayVFX(Vector3 position)
    {
        foreach (GameObject deadVFX in explosionList)
        {
            if(!deadVFX.activeInHierarchy) 
            {
                deadVFX.SetActive(true);
                deadVFX.transform.position = position;
            }
        }

        Invoke("AutoTurnOff", 2f);
    }

    void AutoTurnOff()
    {
        foreach (GameObject deadVFX in explosionList)
        {
            if (deadVFX.activeInHierarchy)
            {
                deadVFX.SetActive(false);
            }
        }
    }
}
