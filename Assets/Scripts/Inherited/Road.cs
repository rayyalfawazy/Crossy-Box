using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] Jeep jeepPrefab;
    [SerializeField] float minVehicleSpawnInterval;
    [SerializeField] float maxVehicleSpawnInterval;

    float timer;
    Vector3 vehicleSpawnPosition;
    Quaternion vehicleRotation;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            vehicleSpawnPosition = new Vector3(horizontalSize / 2 + 3, 0, this.transform.position.z);
            vehicleRotation = Quaternion.Euler(0, -90, 0);
        } else
        {
            vehicleSpawnPosition = new Vector3(-(horizontalSize / 2 + 3), 0, this.transform.position.z);
            vehicleRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    void Update()
    {
        if (timer <= 0) 
        {
            timer = Random.Range(minVehicleSpawnInterval, maxVehicleSpawnInterval);
            var vehicle = Instantiate(jeepPrefab, vehicleSpawnPosition, vehicleRotation);
            vehicle.SetUpDistanceLimit(horizontalSize + 6);
            return;
        }
        timer -= Time.deltaTime;
    }
}
