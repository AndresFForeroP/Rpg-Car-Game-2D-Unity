using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public List<VehicleBase> vehicles;

    void Update()
    {
        vehicles.Sort((a, b) =>
        {
            CarProgress pa = a.GetComponent<CarProgress>();
            CarProgress pb = b.GetComponent<CarProgress>();

            return pb.ProgressValue.CompareTo(pa.ProgressValue);
        });

        for (int i = 0; i < vehicles.Count; i++)
        {
            vehicles[i].position = i + 1;
        }
    }
}