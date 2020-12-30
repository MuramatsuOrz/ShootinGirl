using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
#pragma warning disable CS0618
    void Start()
    {
        //パーティクル終了時に消滅
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
