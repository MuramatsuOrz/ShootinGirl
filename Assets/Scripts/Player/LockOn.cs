using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockOn : MonoBehaviour
{
    //ロックオン対象
    GameObject target = null;
    //ロックオン範囲
    private float targetDistance = 50;

    //デフォルトカーソル
    public Image defaultCursol;
    //ロックオンカーソル
    public Image lockOnCursol;
    //敵のHP表示UI
    public GameObject enemyHP;
    //敵のHPゲージ
    public Image enemyHPImage;
    //敵との距離
    public TextMeshProUGUI enemyDistanceText;

    //ロックオンモードの設定
    //trueの間，周囲の敵を探索し続けて，一番近い敵を自動でロックオンする．
    bool isLockMode = false;

    // Start is called before the first frame update
    void Start()
    {
        //ロックオンモード初期化
        isLockMode = false;
        defaultCursol.enabled = true;
        lockOnCursol.enabled = false;
        enemyHP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ボタンが押されたらターゲット取得
        if(Input.GetButtonDown("Lock")) {
            //モード切り替え
            isLockMode = !isLockMode;

            //ロックオンモードがfalseなら
            if(!isLockMode) {
                //ロック解除
                target = null;
            } else {    //もしロックオンモードなら
                //一番近いターゲット取得
                target = FindClosestEnemy();
            }
        }

        //ロックオンモード中に
        if(isLockMode == true) {
            //もしターゲットがいるなら
            if(target != null) {

                //左右方向計算
                //対象の方向ベクトルを計算
                Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                //プレイヤーが対象の方向を向く
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);
                //X軸，Z軸についてはリセット
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

                //上下方向計算
                //カメラをターゲットに向ける
                Transform cameraParent = Camera.main.transform.parent;
                Quaternion targetRotationCamera = Quaternion.LookRotation(target.transform.position - cameraParent.position);
                cameraParent.localRotation = Quaternion.Slerp(cameraParent.localRotation, targetRotationCamera, Time.deltaTime * 20);
                cameraParent.localRotation = new Quaternion(cameraParent.localRotation.x, 0, 0, cameraParent.localRotation.w);
            } else {
                //ロックオンモードで対象がいなければ，一番近い敵を探索してロックオン
                target = FindClosestEnemy();
            }
        }

        //並行して，距離によるターゲットの持続を判断
        if(target != null) {
            //距離を測り，離れていたらロック解除
            if(Vector3.Distance(target.transform.position, transform.position) > targetDistance) {
                target = null;
            }
        }

        //ターゲットがいたらロックオンカーソルを表示する
        //ロックオン済かどうか
        bool isLocked = false;
        //並行して，ロックオン済かどうか管理
        if(target != null) {

            //ロックオン済にして
            isLocked = true;
            //カーソルを固定
            lockOnCursol.transform.rotation = Quaternion.identity;
            lockOnCursol.transform.localScale = Vector3.Slerp(
                lockOnCursol.transform.localScale,
                new Vector3(1.0f, 1.0f, 1.0f),
                Time.deltaTime * 10);

            //ターゲットの表示位置へ向けてロックオンカーソルを合わせる
            //WorldToScreenPoint()で三次元座標をカメラ上の二次元座標に変換
            lockOnCursol.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);

            //敵の体力をゲージに反映
            Enemy targetEnemyScript = target.GetComponent<Enemy>();
            enemyHPImage.transform.localScale = new Vector3((float)targetEnemyScript.hitPoint / targetEnemyScript.maxHitPoint, 1f, 1f);

            //敵との距離を表示
            //enemyDistanceText.text = Vector3.Distance(target.transform.position, transform.position).ToString();
            enemyDistanceText.text = string.Format("{0:00.00} m", Vector3.Distance(target.transform.position, transform.position));

        } else {
            //ロックオンモードでターゲットを捜索中なら，カーソルを拡大，回転させる
            lockOnCursol.transform.Rotate(0, 0, Time.deltaTime * 200);
            lockOnCursol.transform.localScale = Vector3.Slerp(
                lockOnCursol.transform.localScale,
                new Vector3(1.2f, 1.2f, 1.0f),
                Time.deltaTime * 10);
        }

        //ロックオンカーソル，敵HPの出現を管理
        lockOnCursol.enabled = isLockMode;
        defaultCursol.enabled = !isLockMode;
        enemyHP.SetActive(isLocked);
    }

    //一番近い敵を探して取得する関数
    GameObject FindClosestEnemy() {

        //ゲームオブジェクトリスト
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        //最も近い敵
        GameObject closestEnemy = null;
        //敵との距離
        float distance = Mathf.Infinity;

        foreach(GameObject gameObject in gameObjects) {
            //プレイヤーと敵の距離を計算
            Vector3 difference = gameObject.transform.position - transform.position;
            float currentDistance = difference.sqrMagnitude;

            if(currentDistance < distance) {
                closestEnemy = gameObject;
                distance = currentDistance;
            }
        }

        //処理後，ロックオン対象について
        if(closestEnemy != null) {
            //距離を測り，一番近くの敵が遠いならロックしない
            if(Vector3.Distance(closestEnemy.transform.position, transform.position) > targetDistance) {
                closestEnemy = null;
            }
        }

        return closestEnemy;
    }

}
