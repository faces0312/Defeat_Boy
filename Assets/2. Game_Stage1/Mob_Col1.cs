using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Col1 : MonoBehaviour
{
    public GameObject mob;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            mob.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
