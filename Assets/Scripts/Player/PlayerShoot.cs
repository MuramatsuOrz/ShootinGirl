using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    //弾のプレハブ
    public GameObject shot;
    public GameObject powerShot;
    //弾発射位置
    public GameObject shootSpot;

    //パワーアップエフェクト
    public GameObject powerEffect;
    //パワーアップゲージ
    public Image powerGauge;

    //パワーゲージ
    public float powerPoint;
    public float maxPowerPoint = 5;
    //パワーアップ状態かどうか
    bool isPowerUped = false;

    //発射音
    public AudioClip shotSE;
    //パワーゲージマックス音
    public AudioClip readyPowerUpSE;

    //パワーゲージ通知音管理
    bool isReadySound = false;


    //オーディオソース
    AudioSource[] audioSources;


    //発射間隔時間
    float shotInterval = 0;
    //発射間隔最大値
    float shotIntervalMax = 0.25f;

    private void Start() {
        audioSources = GetComponents<AudioSource>();
        powerEffect.SetActive(false);
        powerPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //発射間隔時間の加算
        shotInterval += Time.deltaTime;

        //パワーアップスイッチ
        if (Input.GetButtonDown("Fire2") && powerPoint == maxPowerPoint) {
            isPowerUped = true;
            //systemAudioSource.PlayOneShot(powerUpSE);
            audioSources[2].Play();
        }
        //パワーアップエフェクト切り替え
        powerEffect.SetActive(isPowerUped);

        //もしパワーアップ中なら，射撃間隔を短くし，パワーアップゲージを減らす
        if(isPowerUped == true) {
            shotIntervalMax = 0.1f;
            powerPoint -= 2* Time.deltaTime;
        } else {
            shotIntervalMax = 0.25f;
            //パワーアップゲージを徐々に上昇
            powerPoint += Time.deltaTime;
        }

        //弾を発射する
        if(shotInterval > shotIntervalMax) {
            if(Input.GetButton("Fire1")) {
                if(isPowerUped == true) {
                    Instantiate(
                        powerShot,                           //強化弾を
                        shootSpot.transform.position,   //発射位置から
                        Camera.main.transform.rotation  //カメラの向きへ
                    );
                } else {
                    Instantiate(
                        shot,                           //弾を
                        shootSpot.transform.position,   //発射位置から
                        Camera.main.transform.rotation  //カメラの向きへ
);
                }

                //audioSource.PlayOneShot(shotSE);
                audioSources[0].PlayOneShot(shotSE);

                //発射間隔リセット
                shotInterval = 0;
            }
        }

        //Clampで上限，下限を設定
        powerPoint = Mathf.Clamp(powerPoint, 0, maxPowerPoint);

        //パワーアップ状態でなく
        if (isPowerUped == false) {
            //パワーゲージがたまったなら，バーを明るくする
            if (powerPoint == maxPowerPoint) {
                powerGauge.color = new Color(255f / 255f, 240f / 255f, 0f);
                //その時，一度だけ音を鳴らす
                if(isReadySound == false) {
                    audioSources[1].Play();
                    isReadySound = true;
                }
            } else {
                powerGauge.color = new Color(150f / 255f, 130f / 255f, 0f);
            }
        }

        //ゲージ残量がなくなったら，パワーアップを終了
        if (powerPoint == 0) {
            isPowerUped = false;
            isReadySound = false;
        }

        //パワーゲージを伸縮
        powerGauge.transform.localScale = new Vector3((float)powerPoint / (float)maxPowerPoint, 1, 1);

    }

}
