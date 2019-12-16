using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenager : MonoBehaviour
{
    public Text ammoText;

    public Text healthText;

    public Text scoreText;
    public float timeMultiplier;
    private float score;

    public GameObject pauseUI;
    private bool gamePaused = false;

    public GameObject deathFX;
    public GameObject deathMenuUI;
    public Text kdRatio;
    private int zombiesKilled;
    private bool playerIsDead = false;
    private bool showed = false;


    void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        score = 0;
        zombiesKilled = 0;
        pauseUI.SetActive(false);
    }

    void Update()
    {
        setScore();
        if (Input.GetButtonDown("Restart")) { RestartLevel(); }
        if (Input.GetButtonDown("Cancel"))
        {
            if (!playerIsDead) { PauseMenu(); }
            if ( playerIsDead) { DeathMenu(); }
        }

    }
    public void setHealth(int health)
    {
        if (health < 0) { health = 0; }
        healthText.text = ""+health;
    }
    public void setAmmo(int ammo)
    {
        ammoText.text = "" + ammo;
    }
    void setScore()
    { 
        if (!gamePaused)
        {
            score += (Time.fixedDeltaTime * timeMultiplier);
            if      (score > 999999999) {   scoreText.text = "" + (long)score; }
            else if (score > 99999999) {   scoreText.text = "0" + (long)score; }
            else if (score > 9999999) {   scoreText.text = "00" + (long)score; }
            else if (score > 999999) {   scoreText.text = "000" + (long)score; }
            else if (score > 99999) {   scoreText.text = "0000" + (long)score; }
            else if (score > 9999) {   scoreText.text = "00000" + (long)score; }
            else if (score > 999) {   scoreText.text = "000000" + (long)score; }
            else if (score > 99) {   scoreText.text = "0000000" + (long)score; }
            else if (score > 9) {   scoreText.text = "00000000" + (long)score; }
            else if (score >= 0) { scoreText.text = "000000000" + (long)score; }
        }

    }
    public void addScore(float addScore)
    {
        this.score += addScore;
        zombiesKilled++;
    }


    public void deathScreen()
    {
        deathFX.SetActive(true);
        DeathMenu();
        gamePaused = true;
        playerIsDead = true;
        kdRatio.text = "K/D: " + zombiesKilled + "/1";
        Time.timeScale = 0f;
    }
    public void DeathMenu()
    {
        if (showed)
        {
            deathMenuUI.SetActive(false);
            showed = false;
        }
        else
        {
            deathMenuUI.SetActive(true);
            showed = true;
        }
    }

    private void PauseMenu()
    {
        if (gamePaused && !playerIsDead)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }


    public void RestartLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    /*
    public void MainMenu()
    {
        StartCoroutine(LoadLevelAsync(0));
    }
    */
    IEnumerator LoadLevelAsync(int nr)
    {
        AsyncOperation loadLevelAsync = SceneManager.LoadSceneAsync(nr);

        //wait until loaded
        while (!loadLevelAsync.isDone)
        {
            yield return null;
        }
    }

}
