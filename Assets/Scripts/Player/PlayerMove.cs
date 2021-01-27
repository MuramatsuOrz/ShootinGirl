using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //移動速度用定数
    //private float speed = 15.0f;
    [SerializeField]
    //ジャンプ速度用定数
    private float jumpSpeed = 30.0f;
    [SerializeField]
    //かかる重力
    private float gravity = 20.0f;
    //移動で動く距離
    private Vector3 moveDirection = Vector3.zero;

    //実際の移動速度
    Vector3 moveSpeed;

    //加速度周りの調整
    const float addNormalSpeed = 1;     //通常時の加算速度
    const float addBoostSpeed = 3;      //ブースト時の加算速度
    private const float moveSpeedMax = 20;      //通常時の最大速度
    private const float boostSpeedMax = 40;     //ブースト時の最大速度

    //ブーストゲージ
    public int boostPoint;
    int maxBoostPoint = 1500;

    //ブーストゲージイメージ
    public Image boostGaugeImage;

    //ブースト状態の管理
    bool isBoost;
    //ジャンプ状態の管理
    bool isJump;

    //オーディオソース(３：ブースト音)
    AudioSource[] audioSources;

    void Start(){
        //ブーストゲージ，速度の初期化
        boostPoint = maxBoostPoint;
        moveSpeed = Vector3.zero;   
        isBoost = false;

        //オーディオソース取得
        audioSources = GetComponents<AudioSource>();

        //カーソルを消して画面に固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        //プレイヤーの移動
        CharacterController controller = GetComponent<CharacterController>();

        //地面についていたら，上方向には移動しない
        if (controller.isGrounded){ 
            moveDirection.y = 0;
        }

        //ブースト時はフラグをたてて，ブーストゲージを減少させる
        if (Input.GetButton("Boost") && boostPoint > 1) {
            boostPoint -= 1;
            isBoost = true;
        } else {
            isBoost = false;
        }

        //ブースト,ジャンプ状態なら，ブースト音をループ再生
        if (isBoost || isJump) {
            if (!audioSources[3].isPlaying) {
                audioSources[3].Play();
            }
        }else {
            audioSources[3].Stop();
        }

        //目標速度
        Vector3 targetSpeed = Vector3.zero;
        //加算速度
        Vector3 addSpeed = Vector3.zero;

        //左右移動時の目標速度と加算速度の算出
        //もし左右に入力がなければ
        if (Input.GetAxis("Horizontal") == 0) {
            //目標速度をゼロにする
            targetSpeed.x = 0;

            //設置しているときと空中にいるときは減速値を変える
            if (controller.isGrounded) {
                addSpeed.x = addNormalSpeed / 4;

            } else {
                addSpeed.x = addNormalSpeed;
            }
                

        } else {
            //通常時とブースト時で変化
            if (isBoost) {
                targetSpeed.x = boostSpeedMax;
                addSpeed.x = addBoostSpeed;
            } else {
                targetSpeed.x = moveSpeedMax;
                addSpeed.x = addNormalSpeed;
            }

            //移動方向の正負判定
            targetSpeed.x *= Mathf.Sign(Input.GetAxis("Horizontal"));
        }

        //左右移動の速度
        //現在の速度から目標に向けて徐々に加速する
        moveSpeed.x = Mathf.MoveTowards(moveSpeed.x, targetSpeed.x, addSpeed.x);
        moveDirection.x = moveSpeed.x;

        //前後移動時の目標速度と加算速度の算出
        //もし前後に入力がなければ
        if (Input.GetAxis("Vertical") == 0) {
            //目標速度をゼロにする
            targetSpeed.z = 0;

            //地上にいるかどうかで加算速度を変更
            if (controller.isGrounded) {
                addSpeed.z = addNormalSpeed;
            } else {
                addSpeed.z = addNormalSpeed / 4;
            }

        } else {
            //通常時とブースト時で変化
            if (isBoost) {
                targetSpeed.z = boostSpeedMax;
                addSpeed.z = addBoostSpeed;
            } else {
                targetSpeed.z = moveSpeedMax;
                addSpeed.z = addNormalSpeed;
            }

            //移動方向の正負判定
            targetSpeed.z *= Mathf.Sign(Input.GetAxis("Vertical"));
        }

        //上下移動の速度
        //現在の速度から目標に向けて徐々に加速する
        moveSpeed.z = Mathf.MoveTowards(moveSpeed.z, targetSpeed.z, addSpeed.z);
        moveDirection.z = moveSpeed.z;

        //計算した上下左右の移動計算結果を入れる
        moveDirection = transform.TransformDirection(moveDirection);


        //ジャンプ操作
        if (Input.GetButton("Jump") && boostPoint > 1) {

            isJump = true;
            //ブーストゲージを減少させる
            boostPoint -= 1;
            //高さ制限以下であれば上昇
            if (transform.position.y > 500) {
                moveDirection.y = 0;
            } else {
                moveDirection.y += jumpSpeed * Time.deltaTime;
            }
        } else {
            //ジャンプキーを離すと落下
            moveDirection.y -= gravity * Time.deltaTime;
            isJump = false;
        }

        //ジャンプ，ブーストしていないなら，ゲージを回復させる
        if(!Input.GetButton("Jump") && !Input.GetButton("Boost")) {
            boostPoint += 3;
        }

        //Clampで上限，下限を設定
        boostPoint = Mathf.Clamp(boostPoint, 0, maxBoostPoint);
        //ブーストゲージを伸縮
        boostGaugeImage.transform.localScale = new Vector3((float)boostPoint / (float)maxBoostPoint, 1, 1);

        //moveDirectionを受けて移動
        controller.Move(moveDirection * Time.deltaTime);
    }
}
