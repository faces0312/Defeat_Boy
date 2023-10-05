using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_DeadLine : MonoBehaviour
{
    public GameObject dead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            dead.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
