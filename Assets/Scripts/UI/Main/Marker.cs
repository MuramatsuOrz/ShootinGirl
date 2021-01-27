using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//マップにマーカーを表示させる
//表示したいオブジェクトにコンポーネントとして付ける
public class Marker : MonoBehaviour
{
    //マーカーインスタンス
    Image marker;
    //生成するマーカーのprefab
    public Image markerImage;
    //表示するレーダー(コンパス)の位置
    GameObject compass;

    //プレイヤー
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの位置を取得
        player = GameObject.Find("PlayerPosition");

        //レーダ(コンパスマスク)を取得
        compass = GameObject.Find("CompassMask");
        //マーカーを表示
        marker = Instantiate(
            markerImage,
            compass.transform.position,
            Quaternion.identity
            );
        //マーカーをコンパスの子オブジェクトにする(スケールは維持)
        marker.transform.SetParent(compass.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        //マーカーとプレイヤーの相対位置を計算
        Vector3 position = transform.position - player.transform.position;
        //3次元→2次元を考慮して,相対座標にマーカー表示
        marker.transform.localPosition = new Vector3(0.5f * position.x, 0.5f * position.z, 0);
    }

    //オブジェクトが消滅したら，マーカーも消滅
    private void OnDestroy() {
        Destroy(marker);
    }
}
