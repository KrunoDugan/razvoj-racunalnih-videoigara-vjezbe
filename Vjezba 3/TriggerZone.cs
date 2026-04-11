using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
    public enum ZoneType { SpeedBoost, GravityChange }
    public ZoneType zoneType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (zoneType == ZoneType.SpeedBoost)
            {
                other.GetComponent<PlayerCollision>().StartCoroutine(SpeedBoost(other));
            }

            if (zoneType == ZoneType.GravityChange)
            {
                Physics.gravity = new Vector3(0, -2f, 0);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (zoneType == ZoneType.GravityChange)
            {
                Physics.gravity = new Vector3(0, -9.81f, 0);
            }
        }
    }

    IEnumerator SpeedBoost(Collider player)
    {
        PlayerCollision pc = player.GetComponent<PlayerCollision>();
        pc.speed *= 2;

        yield return new WaitForSeconds(5);

        pc.speed /= 2;
    }
}
