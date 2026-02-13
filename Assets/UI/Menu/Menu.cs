using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    
    [SerializeField] Button playbutton;
    [SerializeField] Button moreganesbutton;
    [SerializeField] Button exitbutton;
    [SerializeField] private GameObject menu;
    [SerializeField] GameObject tutorial;
    
    

    void Start()
    {
        playbutton.onClick.AddListener(buttonplaypressed);
        moreganesbutton.onClick.AddListener(buttonmoregamespressed);
        exitbutton.onClick.AddListener(buttonexitpressed);
    }
    public void buttonplaypressed()
    {
        menu.SetActive(false); 
        tutorial.SetActive(true);
    }
    public void buttonmoregamespressed()
    {
        Application.OpenURL("www.linkedin.com/in/andres-forero-pérez-34a4563aa");
    }
    public void buttonexitpressed()
    {
        Application.Quit();
    }

}
