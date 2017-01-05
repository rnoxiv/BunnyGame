using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject audioOnIcon;
    public GameObject audioOffIcon;
    public Text bestScore;

	// Use this for initialization
	void Start () {
        SetSoundState();
        bestScore.text = PlayerPrefs.GetFloat("BestScore", 0).ToString("0.0");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
            PlayerPrefs.SetInt("Muted", 1);
        else
            PlayerPrefs.SetInt("Muted", 0);
        SetSoundState();
    }

    private void SetSoundState()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            audioOffIcon.SetActive(false);
            audioOnIcon.SetActive(true);
        }else
        {
            AudioListener.volume = 0;
            audioOffIcon.SetActive(true);
            audioOnIcon.SetActive(false);
        }
    }
}
