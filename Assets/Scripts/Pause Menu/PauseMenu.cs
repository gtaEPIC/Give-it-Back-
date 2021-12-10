using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;
    private GameObject[] _pauseObjects;

    public void Start()
    {
        _pauseObjects = GameObject.FindGameObjectsWithTag("PauseMenu");
        Resume();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1;
        foreach (var item in _pauseObjects)
        {
            item.SetActive(false);
        }
    }
    
    private void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0;
        foreach (var item in _pauseObjects)
        {
            item.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
