using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject lorePanel;


    void Start()
    {
        lorePanel.SetActive(false);
    }

    void Update()
    {

    }

    public void ActivateLorePanel(bool activate)
    {
        lorePanel.SetActive(activate);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
