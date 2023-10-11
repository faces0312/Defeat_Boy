using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Stage1 : MonoBehaviour
{
    public static GM_Stage1 gm;
    public Player_Stage1 player;

    public RectTransform stamina;
    public RectTransform stamina_loading;

    public RectTransform hp;

    public int mob_Cnt;
    public GameObject first_Area;


    private void Awake()
    {
        gm = this;
        mob_Cnt = 0;
    }

    private void Update()
    {
        if(hp.localScale.x <= 0)
        {
            Time.timeScale = 0;
        }

        if(mob_Cnt >= 5)
        {
            first_Area.gameObject.SetActive(false);
        }
    }
}
