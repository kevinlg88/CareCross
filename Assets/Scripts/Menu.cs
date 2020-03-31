using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Awake() 
    {
        Time.timeScale = 1f;    
    }
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Tema");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
