using UnityEngine;

public class CarProgress : MonoBehaviour
{
    public int currentWaypoint = 0;
    public int lastWaypoint = -1;
    public int lap = 1;

    public int totalWaypoints = 8;

    public float ProgressValue =>
        lap * totalWaypoints + currentWaypoint;
}