using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginMatch : MonoBehaviour
{
    public static BeginMatch current;
    GameObject pauseButton;
    private void Start()
    {
        Time.timeScale = 0;
        pauseButton = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseButton.SetActive(true);
    }

    public void BeginGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseButton.SetActive(false);
    }
}
