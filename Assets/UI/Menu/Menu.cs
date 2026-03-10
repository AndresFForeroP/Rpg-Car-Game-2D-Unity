using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    
    [SerializeField] Button playbutton;
    [SerializeField] Button moreganesbutton;
    [SerializeField] Button exitbutton;
    [SerializeField] GameObject exitbuttogame;
    [SerializeField] private GameObject menu;
    [SerializeField] GameObject tutorial;
    
    

    void Start()
    {    
        playbutton.onClick.AddListener(buttonplaypressed);
        moreganesbutton.onClick.AddListener(buttonmoregamespressed);
        exitbutton.onClick.AddListener(buttonexitpressed);
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            exitbuttogame.SetActive(false);
    }
    public void buttonplaypressed()
    {
        menu.SetActive(false); 
        tutorial.SetActive(true);
    }
    public void buttonmoregamespressed()
    {
        Application.OpenURL("https://www.linkedin.com/in/andres-forero-p%C3%A9rez-34a4563aa/");
    }
    public void buttonexitpressed()
    {
        Application.Quit();
    }

}
