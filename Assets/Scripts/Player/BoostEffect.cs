using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    public GameObject boostEffect;
    private PlayerMove playerMove;
    public int boostPoint;

    // Start is called before the first frame update
    void Start()
    {
        boostEffect.SetActive(false);
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isBoost = false;
        boostPoint = playerMove.boostPoint;

        if ((Input.GetButton("Boost") || Input.GetButton("Jump")) && boostPoint > 1) {
            isBoost = true;
        }

        boostEffect.SetActive(isBoost);
    }
}
