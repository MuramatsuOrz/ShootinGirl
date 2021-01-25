using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    //爆発エフェクト
    public GameObject explosion;

    //弾速
    private float shotSpeed = 100;

    //ダメージ量
    public readonly int damage = 200;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        //一定時間後に消滅
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update() {
        //弾を前進させる
        rb.velocity = transform.forward * shotSpeed;
    }


    private void OnCollisionEnter(Collision collider) {
        //プレイヤーとぶつかったら
        if(collider.gameObject.CompareTag("Objects") ||collider.gameObject.CompareTag("Player")) {

            //消滅させる
            Destroy(gameObject);
            //爆発を生成
            Instantiate(
                explosion,
                transform.position,
                transform.rotation
                );
        }
    }

}
