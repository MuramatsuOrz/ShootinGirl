using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSystem : MonoBehaviour
{
    public GameObject title;
    public GameObject option;
    public AudioMixer audioMixer;

    //Mainシーンへ遷移する関数
    private void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

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

    public void OptionUIActive() {
        title.gameObject.SetActive(false);
        option.gameObject.SetActive(true);
    }

    public void OptionUIFalse() {
        title.gameObject.SetActive(true);
        option.gameObject.SetActive(false);
    }

    public void SetMaster(float volume) {
        audioMixer.SetFloat("MasterVol", volume);
    }

    public void SetBGM(float volume) {
        audioMixer.SetFloat("BGMVol", volume);
    }

    public void SetSE(float volume) {
        audioMixer.SetFloat("SEVol", volume);
    }
}

