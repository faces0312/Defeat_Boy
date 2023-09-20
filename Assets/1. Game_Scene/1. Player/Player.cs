using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator player_Animator;//�ִϸ��̼�

    public bool is_roll;//������ �ִ���
    public float speed;//�ӵ�


    public float xMove;//�¿� �Է�
    public float yMove;//���� �Է�
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
        //������ ����
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

        //������ �ִϸ��̼�
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            player_Animator.SetBool("Is_Run", true);
        }
        else
        {
            player_Animator.SetBool("Is_Run", false);
        }

        //�¿� ����
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
