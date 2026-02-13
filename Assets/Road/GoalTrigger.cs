using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public int lastWaypointIndex = 7;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarProgress car = other.GetComponent<CarProgress>();
        VehicleBase vehicle = other.GetComponent<VehicleBase>();

        if (car == null || vehicle == null) return;


        if (car.lastWaypoint == lastWaypointIndex)
        {
            car.lap++;
            vehicle.lap = car.lap; 
            car.lastWaypoint = -1;
        }
    }
}