using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject panelUI;
    public GameObject birdPositionShow;

    public Text scoreText;
    public Text bestScoreText;

    public TextMeshProUGUI congratsText;
    public GameObject meshProCongratsText;

    void Awake()
    {
        if (instance == null)
            instance = this;

        congratsText = meshProCongratsText.GetComponent<TextMeshProUGUI>();
    }


    private void Start()
    {
        //congratsText.enabled = false;
    }


    private void FixedUpdate()
    {
        
    }


    public void GameOverMenu(int score)
    {
        panelUI.SetActive(true);
        //Time.timeScale = 0f;
        if (PlayerPrefs.GetInt("BestScore") <= score)
        {
            PlayerPrefs.SetInt("BestScore", score);
            
        }
    }

    public void LoadGame()
    {
        panelUI.SetActive(false);

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return;

        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore");
    }

    public void addScore(int score)
    {
        scoreText.text = "Score: " + score;
        if (PlayerPrefs.GetInt("BestScore") < score)
            bestScoreText.text = "Best Score: " + score;
        else bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore");


    }


    public void OpenSettingsPanel()
    {
        panelUI.SetActive(true);
        Time.timeScale = 0f;
    }


    public void ShowWhereBird()
    {
        //GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
        //if(birds != null)
        //{
        //    birdPositionShow.SetActive(true);
        //    birdPositionShow.transform.Translate(Vector3.right * birds[0].transform.position.x);
        //}

       
    }


    public void ShowTextCongrats(string textToSet)
    {

        congratsText.text = textToSet;
        meshProCongratsText.GetComponent<Animator>().SetTrigger("text");

    }


   //public IEnumerator ShowTextAnimation()
   // {
   //     yield return new WaitForSeconds(0.4f);
   // }

}
