using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //弾速
    private float shotSpeed = 100;

    //ダメージ量
    public readonly int damage = 50;

    //Rigidbody    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        //一定時間後に消滅
        Destroy(gameObject, 4.0f);
    }

    // Update is called once per frame
    void Update() {
        //弾を前進させる
        rb.velocity = transform.forward * shotSpeed;
    }


    private void OnTriggerEnter(Collider other) {
        //地形とぶつかったら
        if (other.gameObject.CompareTag("Objects")) {
            //消滅させる
            Destroy(gameObject);
        }
    }
}
