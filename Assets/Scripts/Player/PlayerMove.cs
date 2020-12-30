using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    //移動速度
    private float speed = 14.0f;
    [SerializeField]
    //ジャンプ速度
    private float jumpSpeed = 15.0f;
    [SerializeField]
    //かかる重力
    private float gravity = 20.0f;

    //移動方向
    private Vector3 moveDirection = Vector3.zero;

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //プレイヤーの移動させる
        CharacterController controller = GetComponent<CharacterController>();

        if(controller.isGrounded) {
            //入力を受けて方向決定
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //ワールド座標に変換して移動値とする
            moveDirection = transform.TransformDirection(moveDirection);
            //速度をかけてどれだけ移動するか設定
            moveDirection *= speed;

            if(Input.GetButton("Jump")) {
                //ジャンプボタンが押されたら，y方向に飛ぶ
                moveDirection.y = jumpSpeed;
            }
        }

        //重力にしたがって落ちる
        moveDirection.y -= gravity * Time.deltaTime;

        //moveDirectionを受けて移動
        controller.Move(moveDirection * Time.deltaTime);
    }
}
