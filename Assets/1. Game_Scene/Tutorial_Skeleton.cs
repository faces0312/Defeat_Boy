using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Skeleton : MonoBehaviour
{
    public GameObject tutorial_Skeleton;

    // Update is called once per frame
    void Update()
    {
        if(tutorial_Skeleton.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }
}
