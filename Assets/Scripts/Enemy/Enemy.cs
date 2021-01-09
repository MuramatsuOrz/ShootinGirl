using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //弾
    public GameObject shot;
    //発射位置
    public GameObject shootSpot;
    //発射間隔時間
    private float shotInterval = 0;
    //発射間隔最大値
    private float shotIntervalMax = 1.0f;

    //プレイヤーの位置
    private GameObject targetPosition;

    //プレイヤー認識までの距離
    private float maxDistance = 75f;

    //破壊された時のエフェクト
    public GameObject brokeEffect;
    //RigidBody
    Rigidbody rb;

    //体力
    public int hitPoint;
    //体力最大値
    public readonly int maxHitPoint = 500;

    //敵の移動速度
    private float enemySpeed = 20f;

    //発射音
    public AudioClip shotSE;
    //オーディオソース
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
        //プレイヤーの位置を探索
        targetPosition = GameObject.Find("PlayerPosition");
        //体力を初期化
        hitPoint = maxHitPoint;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //もしプレイヤーが認識範囲内なら
        if(Vector3.Distance(targetPosition.transform.position, transform.position) <= maxDistance) {

            //プレイヤーの方向を向いた時の回転を算出し
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition.transform.position - transform.position);
            //緩やかにそちらを向く
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            //プレイヤーに近づく                
            rb.velocity = transform.forward * enemySpeed;

            //プレイヤーとの距離が近すぎるなら
            if (Vector3.Distance(targetPosition.transform.position, transform.position) <= 10) {
                //速度をゼロにする
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }


            //発射間隔時間の加算
            shotInterval += Time.deltaTime;
            //時間が経ったら
            if(shotInterval > shotIntervalMax) {
                Instantiate(
                    shot,                           //弾を
                    shootSpot.transform.position,   //発射位置から
                    shootSpot.transform.rotation  //カメラの向きへ
                );
                //発射間隔リセット
                shotInterval = 0;
                //射撃音を出す
                audioSource.PlayOneShot(shotSE);
            }
        } else {
            //範囲外なら動かない
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //プレイヤーの球と衝突したら
        if(collision.gameObject.CompareTag("Shot")) {

            //プレイヤーの球からダメージ算出
            int damage = collision.gameObject.GetComponent<ShotPlayer>().damage;

            //ダメージを受ける
            hitPoint -= damage;
            //Debug.Log("HP = " + hitPoint);

            //もしHPがゼロなら
            if(hitPoint <= 0) {
                //自身を破壊
                Destroy(gameObject);
                //爆発エフェクト生成
                Instantiate(
                    brokeEffect,
                    transform.position,
                    transform.rotation
                    );
                //リザルトスコアを加算
                GameManager.score++;
            }
        }
    }

}
