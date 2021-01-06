using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiateManager : MonoBehaviour
{
    //タイマー
    private float timer = 3;
    //敵の生成間隔
    private float instantiateInterval = 3;

    //生成する敵の数
    private int instantiateEnemyValue = 50;

    //Enemy prefab
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        //一定の時間間隔で
        if(timer < 0) {
            //生成する敵の数が残っているなら
            if(instantiateEnemyValue > 0) {
                //敵をランダムな位置に生成
                Instantiate(
                    enemy,
                    new Vector3(Random.Range(-50f, 50), Random.Range(7, 25f), Random.Range(-50, 50)),
                    Quaternion.identity
                    );
                //生成する敵の数を減らす
                instantiateEnemyValue--;
            }

            //生成間隔をだんだんと短くする
            instantiateInterval -= 0.1f;
            instantiateInterval = Mathf.Clamp(instantiateInterval, 1.0f, float.MaxValue);

            //生成間隔をタイマーに代入して，再度時間計測
            timer = instantiateInterval;
        }
    }

}
