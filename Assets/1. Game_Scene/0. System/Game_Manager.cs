using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game_Manager : MonoBehaviour
{
    static Game_Manager gm;
    public Player player;

    public RectTransform stamina;

    private void Awake()
    {
        gm = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
