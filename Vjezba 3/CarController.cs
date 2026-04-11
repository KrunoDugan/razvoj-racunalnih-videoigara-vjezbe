using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour
{
    public GameObject door;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        door.transform.position += new Vector3(0, 3, 0);

        yield return new WaitForSeconds(3);

        door.transform.position -= new Vector3(0, 3, 0);
    }
}
