using UnityEngine;

public class WaypointTrigger : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarProgress car = other.GetComponent<CarProgress>();
        if (car == null) return;

        if (index == car.currentWaypoint)
        {
            car.lastWaypoint = car.currentWaypoint;
            car.currentWaypoint++;

            if (car.currentWaypoint >= car.totalWaypoints)
                car.currentWaypoint = 0;
        }
    }
}