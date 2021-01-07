using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
    //Mainシーンへ遷移する関数
    public void StartGame() {
        SceneManager.LoadScene("Main");
    }

    //アプリケーションを終了する関数
    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}

