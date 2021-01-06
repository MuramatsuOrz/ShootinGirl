using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour
{
    //爆発エフェクト
    public GameObject explosion;

    //弾速
    private float shotSpeed = 200;

    //ダメージ量
    public readonly int damage = 200;

    // Start is called before the first frame update
    void Start()
    {
        //一定時間後に消滅
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //弾を前進させる
        transform.position += transform.forward * Time.deltaTime * shotSpeed;

    }


    private void OnCollisionEnter(Collision collider) {
        //地形とぶつかったら
        if(collider.gameObject.CompareTag("Objects") || collider.gameObject.CompareTag("Enemy")) {
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
