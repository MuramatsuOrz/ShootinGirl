using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;

//Mainシーンのゲーム状態を管理
public class GameManager : MonoBehaviour
{
    //状態を代入する変数
    public static int battleStatus;

    //状態
    const int BATTLE_START = 0;
    const int BATTLE_PLAY = 1;
    const int BATTLE_END = 2;

    //タイマー
    float timer = 0;

    //倒した敵の数を数える変数
    public static int score;

    int clearScore = 0;

    //メッセージ
    public TextMeshProUGUI startMessage;
    public TextMeshProUGUI winMessage;
    public TextMeshProUGUI loseMessage;

    // 状態の初期化
    private void Start()
    {
        battleStatus = BATTLE_START;
        timer = 0;
        score = 0;
        clearScore = EnemyInstantiateManager.instantiateEnemyValue;
        startMessage.gameObject.SetActive(true);
        winMessage.gameObject.SetActive(false);
        loseMessage.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStatus) {
            case BATTLE_START:
                //時間計測
                timer += Time.deltaTime;

                //プレイ状態に移行
                if(timer > 3) {
                    startMessage.gameObject.SetActive(false);
                    battleStatus = BATTLE_PLAY;
                    timer = 0;
                }

                break;

            case BATTLE_PLAY:
                //すべての敵を倒すことができたらゲーム終了
                if(score >= clearScore) {
                    battleStatus = BATTLE_END;
                    winMessage.gameObject.SetActive(true);
                }
                //プレイヤーのHPがゼロでも終了
                if(PlayerHP.hitPoint <= 0) {
                    battleStatus = BATTLE_END;
                    loseMessage.gameObject.SetActive(true);
                }
                break;

            case BATTLE_END:
                //一定時間経過で，タイトルに遷移可能にする
                timer += Time.deltaTime;

                if(timer > 3) {
                    //シーンの動作を停止
                    Time.timeScale = 0;

                    if (Input.GetButton("Cancel")) {
                        SceneManager.LoadScene("Title");

                        //動作再開
                        Time.timeScale = 1;
                    }
                }
                break;

            default:
                break;
        }

    }

}
