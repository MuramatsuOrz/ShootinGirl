﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyInstantiateManager : MonoBehaviour
{
    //タイマー
    private float timer = 2;
    //敵の生成間隔
    private float instantiateInterval = 2;
    //敵の最小生成間隔
    private float minInstantiateInterval = 0.3f;

    //生成する敵の数
    public static int instantiateEnemyValue = 10;

    //Enemy prefab
    public GameObject enemy;

    //ランダムな座標を入れる変数
    private float randX;
    private float randY;
    private float randZ;

    //レイキャスト
    RaycastHit hit;

    // Start is called before the first frame update
    void Awake()
    {
        instantiateEnemyValue = 50;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        //一定の時間間隔で
        if(timer < 0) {
            //生成する敵の数が残っているなら
            if(instantiateEnemyValue > 0) {
                //ランダムなX,Zを生成
                randX = Random.Range(-50f, 50f);
                randY = Random.Range(25f, 100f);
                randZ = Random.Range(-50f, 50f);

                if(!Physics.SphereCast(new Vector3(randX,randY,randZ),4, Vector3.down, out hit, 50, LayerMask.GetMask("Player","Enemy","Stage"))) {
                    Instantiate(
                        enemy,
                        new Vector3(randX,randY,randZ),
                        Quaternion.identity
                        );
                    //生成する敵の数を減らす
                    instantiateEnemyValue--;
                }
            }

            //生成間隔をだんだんと短くする
            instantiateInterval -= 0.1f;
            instantiateInterval = Mathf.Clamp(instantiateInterval, minInstantiateInterval, float.MaxValue);

            //生成間隔をタイマーに代入して，再度時間計測
            timer = instantiateInterval;
        }
    }

}
