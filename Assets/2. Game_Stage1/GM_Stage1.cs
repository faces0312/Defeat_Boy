using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Stage1 : MonoBehaviour
{
    static GM_Stage1 gm;
    public Player_Stage1 player;

    public RectTransform stamina;
    public RectTransform stamina_loading;

    public RectTransform hp;


    private void Awake()
    {
        gm = this;
    }

    private void Update()
    {
        if(hp.localScale.x <= 0)
        {
            Time.timeScale = 0;
        }
    }
}
