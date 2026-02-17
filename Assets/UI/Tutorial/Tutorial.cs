using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public bool Play = false;
    [SerializeField] Button playbuttontutorial;
    [SerializeField] AudioSource AudioMenu;
    [SerializeField] AudioSource AudioRace;
    [SerializeField] GameObject GameUi;
    [SerializeField] EnemyCar enemyCar;
    [SerializeField] PlayerCar playerCar;
    void Start()
    {
        playbuttontutorial.onClick.AddListener(buttonplaypressedtutorial);
        StartCoroutine(InteractableButton());
    }
    public void buttonplaypressedtutorial()
    {
        gameObject.SetActive(false);
        Play = true;
        AudioMenu.Stop();
        AudioRace.Play();
        GameUi.SetActive(true);
        enemyCar.agent.isStopped = false;
        playerCar.acceleration = 0.10f;
        playerCar.steeringSpeed = 200;
    }
    IEnumerator InteractableButton()
    {
        yield return new WaitForSeconds(6f);
        playbuttontutorial.interactable= true;
    }

}
