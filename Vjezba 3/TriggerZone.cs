using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
    public enum ZoneType
    {
        SpeedBoost,
        GravityChange
    }

    public ZoneType zoneType;

    public float boostMultiplier = 2f;
    public float boostDuration = 5f;
    public Vector3 customGravity = new Vector3(0f, -2f, 0f);

    private Vector3 defaultGravity;

    void Start()
    {
        defaultGravity = Physics.gravity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (zoneType == ZoneType.SpeedBoost)
        {
            StartCoroutine(SpeedBoost(other));
        }

        if (zoneType == ZoneType.GravityChange)
        {
            Physics.gravity = customGravity;
            Debug.Log("Gravity changed!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (zoneType == ZoneType.GravityChange)
        {
            Physics.gravity = defaultGravity;
            Debug.Log("Gravity restored!");
        }
    }

    IEnumerator SpeedBoost(Collider player)
    {
        CarController car = player.GetComponent<CarController>();

        if (car != null)
        {
            car.speed *= boostMultiplier;
            Debug.Log("Speed boost activated!");

            yield return new WaitForSeconds(boostDuration);

            car.speed /= boostMultiplier;
            Debug.Log("Speed boost ended!");
        }
    }
}
