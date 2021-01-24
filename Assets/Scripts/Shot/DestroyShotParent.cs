using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyShotParent : MonoBehaviour
{
    //子オブジェクトを保存するリスト
    [SerializeField]
    private int childCount;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        childCount = this.transform.childCount;
        if(childCount == 0) {
            Destroy(this.gameObject);
        }
    }
}
