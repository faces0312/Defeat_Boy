using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game_Manager : MonoBehaviour
{
    static Game_Manager gm;
    public Player player;

    public RectTransform stamina;
    public RectTransform stamina_loading;

    public RectTransform hp;


    private void Awake()
    {
        gm = this;
    }

}
