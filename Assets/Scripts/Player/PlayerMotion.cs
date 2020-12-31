using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playerMove;
    public int boostPoint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        //ブーストポイント監視
        boostPoint = playerMove.boostPoint;

        //モーションの切り替え
        //左右移動
        if (Input.GetAxis("Horizontal") > 0) {
            animator.SetInteger("Horizontal", 1);
        }else if(Input.GetAxis("Horizontal") < 0){
            animator.SetInteger("Horizontal", -1);
        }else {
            animator.SetInteger("Horizontal", 0);
        }

        //前後移動
        if(Input.GetAxis("Vertical") > 0) {
            animator.SetInteger("Vertical", 1);
        }else if(Input.GetAxis("Vertical") < 0) {
            animator.SetInteger("Vertical", -1);
        } else {
            animator.SetInteger("Vertical", 0);
        }

        if(controller.isGrounded) {
            animator.SetBool("Jump", false);
        }

        //ジャンプ
        if(boostPoint > 1) {
            animator.SetBool("Jump", Input.GetButton("Jump"));
        } else {
            animator.SetBool("Jump", false);
        }
        //ブースト
        if (boostPoint > 1) {
            animator.SetBool("Boost", Input.GetButton("Boost"));
        } else {
            animator.SetBool("Boost", false);
        }
    }

}
