using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob_Stage1 : MonoBehaviour
{
    public GM_Stage1 gm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            gm.hp.localScale = new Vector3(0, gm.hp.localScale.y);
        }
    }
}
