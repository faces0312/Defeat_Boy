using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator player_Animator;//애니메이션

    public bool is_roll;//구르고 있는지
    public float speed;//속도


    public float xMove;//좌우 입력
    public float yMove;//상하 입력
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player_Animator.SetTrigger("Is_Roll");
        }

        Move();
    }

    public void Move()
    {
        //움직임 구현
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }

        //움직임 애니메이션
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            player_Animator.SetBool("Is_Run", true);
        }
        else
        {
            player_Animator.SetBool("Is_Run", false);
        }

        //좌우 반전
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.localScale = new Vector2(0.8f, 0.8f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.localScale = new Vector2(-0.8f, 0.8f);
        }
    }
}
