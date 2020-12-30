using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        //モーションの切り替え
        //左右移動
        if(Input.GetAxis("Horizontal") > 0) {
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
        animator.SetBool("Jump", Input.GetButton("Jump"));
    }

}
