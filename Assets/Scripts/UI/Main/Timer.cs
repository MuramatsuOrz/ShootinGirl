﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	//　前のUpdateの時の秒数
	private float oldSeconds;
	//　タイマー表示用テキスト
	public TextMeshProUGUI timerText;

	void Start() {
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
	}

	void Update() {

		seconds += Time.deltaTime;

		if (seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}
		//値が変わった時だけテキストUIを更新
		if ((int)seconds != (int)oldSeconds) {
			timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
		}
		oldSeconds = seconds;
	}
}
