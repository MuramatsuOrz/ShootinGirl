using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //弾のプレハブ
    public GameObject shot;
    //発射位置
    public GameObject shootSpot;

    //発射音
    public AudioClip shotSE;
    //オーディオソース
    AudioSource audioSource;

    //発射間隔時間
    float shotInterval = 0;
    //発射間隔最大値
    float shotIntervalMax = 0.25f;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //発射間隔時間の加算
        shotInterval += Time.deltaTime;

        //弾を発射する
        if(shotInterval > shotIntervalMax) {
            if(Input.GetButton("Fire1")) {
                Instantiate(
                    shot,                           //弾を
                    shootSpot.transform.position,   //発射位置から
                    Camera.main.transform.rotation  //カメラの向きへ
                );
                audioSource.PlayOneShot(shotSE);

                //発射間隔リセット
                shotInterval = 0;
            }
        }
    }

}
