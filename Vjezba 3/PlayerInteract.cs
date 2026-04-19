using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour
{
    public GameObject door;
    public float openHeight = 3f;
    public float openDuration = 3f;

    private bool isMoving = false;
    private Vector3 closedPosition;
    private Vector3 openedPosition;

    void Start()
    {
        if (door != null)
        {
            closedPosition = door.transform.position;
            openedPosition = closedPosition + new Vector3(0f, openHeight, 0f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isMoving && door != null)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isMoving = true;

        door.transform.position = openedPosition;

        yield return new WaitForSeconds(openDuration);

        door.transform.position = closedPosition;

        isMoving = false;
    }
}
