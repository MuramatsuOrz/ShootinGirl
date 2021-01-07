using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
	int hitPoint;
	int maxHitPoint = 5000;

	int damage;

	public TextMeshProUGUI hitPointText;
    //public Text hitPointText;

    int displayHitPoint;

	public Image gaugeImage;

	// Use this for initialization
	void Start() {

		//表示するHPを最大値に初期化
		hitPoint = maxHitPoint;
		displayHitPoint = hitPoint;

	}

	// Update is called once per frame
	void Update() {

		//現在の体力と表示用体力が異なっていれば、現在の体力になるまで加減算する
		if(displayHitPoint != hitPoint) {
			displayHitPoint = (int)Mathf.Lerp(displayHitPoint, hitPoint, 0.1f);
		}

        //現在HPと最大HPをUI Textに表示する
        hitPointText.SetText(string.Format("{0:0000} / {1:0000}", displayHitPoint, maxHitPoint));

        //残り体力の割合により，文字とHPバーの色を変える
        float percentageHitPoint = (float)displayHitPoint / maxHitPoint;
		if(percentageHitPoint > 0.5f) {
            hitPointText.color = new Color(0f, 1f, 25f / 255f);
			gaugeImage.color = new Color(0f, 1f, 25f / 255f);

		} else if(percentageHitPoint > 0.25f) {
			hitPointText.color = new Color(1f, 245f / 255f, 0f);
			gaugeImage.color = new Color(1f, 245f / 255f, 0f);
		} else {
			hitPointText.color = new Color(1f, 0f, 100f / 255f);
			gaugeImage.color = new Color(1f, 0f, 100f / 255f);
		}

		//ゲージの長さを体力の割合に合わせて伸縮させる
		gaugeImage.transform.localScale = new Vector3(percentageHitPoint, 1, 1);
	}

	private void OnCollisionEnter(Collision collision) {

		//敵の弾と衝突したらダメージ
		if(collision.gameObject.CompareTag("ShotEnemy")) {

            ////敵の球からダメージ算出
            damage = collision.gameObject.GetComponent<ShotEnemy>().damage;
            hitPoint -= damage;
			hitPoint = Mathf.Clamp(hitPoint, 0, maxHitPoint);
		}
	}
}
