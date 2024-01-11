using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Player player;

    private bool dir_right;
    private void OnEnable()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.7f);
        
        if (player.move_right == true)
        {
            dir_right = true;
            gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        }
        else
        {
            dir_right = false;
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
    }

    void Update()
    {
        if(dir_right == true)
            transform.Translate(Vector3.right * 10 * Time.deltaTime);
        else
            transform.Translate(Vector3.left * 10 * Time.deltaTime);
    }
}
