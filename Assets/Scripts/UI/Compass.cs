using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    //回転する画像
    public Image compassImage;
    //基準となるプレイヤー
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //方角を回転
        //三次元のプレイヤーy軸回転を，二次元のコンパス画像z軸回転に渡すことで，回転を実現
        compassImage.transform.rotation = Quaternion.Euler(
            compassImage.transform.rotation.x,
            compassImage.transform.rotation.y,
            player.transform.eulerAngles.y);
    }
}
