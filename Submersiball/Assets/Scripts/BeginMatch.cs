using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginMatch : MonoBehaviour
{
    [SerializeField] KeyCode pauseKey;
    GameObject pauseButton;
    private void Start()
    {
        Time.timeScale = 0;
        pauseButton = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseButton.SetActive(true);
        }
    }

    public void BeginGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseButton.SetActive(false);
    }
}
