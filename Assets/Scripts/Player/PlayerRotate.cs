using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの回転方向を管理
public class PlayerRotate : MonoBehaviour
{
    //カメラの親オブジェクト
    GameObject cameraParent;
    //カメラの初期回転位置
    Quaternion defaultCameraRotation;
    //タイマー
    float timer = 0;

    //回転速度調整用
    private float rotateSpeed = 1;

    private void Start() {
        //カメラの初期方向の保存
        cameraParent = Camera.main.transform.parent.gameObject;
        defaultCameraRotation = cameraParent.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー自身を左右回転
        transform.Rotate(0, Input.GetAxis("Horizontal2") * rotateSpeed, 0);

        //カメラの親を回転させて上下視点移動
        //カメラの親取得
        GameObject cameraParent = Camera.main.transform.parent.gameObject;

        //範囲設定
        float maxLimit = 45;
        float minLimit = 300;   //360 - 最大仰角
        //入力取得
        var _inputY = Input.GetAxis("Vertical2");

        //ローカル角度取得，入力に合わせて角度加算
        var localAngle = cameraParent.transform.localEulerAngles;
        localAngle.x += _inputY;

        //範囲を超えていたら値を修正
        //localEulerAnglesのx,y,zは時計回りに0°～360°の値になっているので，180°前後の条件も付ける
        if(localAngle.x > maxLimit && localAngle.x < 180)
            localAngle.x = maxLimit;
        if(localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;

        //最終的な回転角度を代入し直す
        cameraParent.transform.localEulerAngles = localAngle;

        //カメラを上下移動
        cameraParent.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);

        //カメラのリセット
        if(Input.GetButton("CameraReset")) {
            timer = 0.5f;
        }
        //スムーズにカメラの回転を戻す
        if(timer > 0) {
            cameraParent.transform.localRotation = Quaternion.Slerp(cameraParent.transform.localRotation, defaultCameraRotation, Time.deltaTime * 10);

            timer -= Time.deltaTime;
        }
    }
}
