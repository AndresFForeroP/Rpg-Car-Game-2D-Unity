using TMPro;
using UnityEngine;

public class GameUIScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerCar playerCar;

    [Header("Repair Icons (Order: 1 -> 3)")]
    [SerializeField] private GameObject[] repairIcons;

    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI lapText;
    [SerializeField] private TextMeshProUGUI positionText;

    private int lastLap = -1;
    private int lastPosition = -1;
    private int lastRepairs = -1;

    private void Update()
    {
        UpdateLapUI();
        UpdatePositionUI();
        UpdateRepairsUI();
    }

    private void UpdateLapUI()
    {
        if (playerCar.lap != lastLap)
        {
            lastLap = playerCar.lap;
            lapText.text = lastLap.ToString();
        }
    }

    private void UpdatePositionUI()
    {
        if (playerCar.position != lastPosition)
        {
            lastPosition = playerCar.position;
            positionText.text = lastPosition.ToString();
        }
    }

    private void UpdateRepairsUI()
    {
        if (playerCar.Repairs != lastRepairs)
        {
            lastRepairs = playerCar.Repairs;

            for (int i = 0; i < repairIcons.Length; i++)
            {
                repairIcons[i].SetActive(i < lastRepairs);
            }
        }
    }
}