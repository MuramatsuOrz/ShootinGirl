using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCount : MonoBehaviour
{
    public static int count = 0;
    int oldCount = 0;
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Awake()
    {
        count = EnemyInstantiateManager.instantiateEnemyValue;
        oldCount = EnemyInstantiateManager.instantiateEnemyValue;
        countText.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {        
        //　値が変わった時だけテキストUIを更新
        if ((int)count != (int)oldCount) {
            countText.text = count.ToString();
        }
        oldCount = count;
    }
}

