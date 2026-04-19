using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float turnSpeed = 80f;

    [Header("Damage System")]
    public int maxHits = 3;
    public float hitCooldown = 1f;

    private int hitCount = 0;
    private bool canTakeHit = true;
    private bool isDestroyed = false;

    [Header("Effects")]
    public GameObject smallFireEffect;
    public GameObject bigFireEffect;
    public GameObject explosionPrefab;

    [Header("Audio")]
    public AudioSource explosionSound;

    [Header("Camera")]
    public CameraShake cameraShake;

    private Rigidbody rb;
    private MeshRenderer[] renderers;
    private Collider[] allColliders;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        allColliders = GetComponentsInChildren<Collider>();

        if (smallFireEffect != null)
            smallFireEffect.SetActive(false);

        if (bigFireEffect != null)
            bigFireEffect.SetActive(false);
    }

    void Update()
    {
        if (isDestroyed) return;

        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        transform.Translate(0f, 0f, move);
        transform.Rotate(0f, turn, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDestroyed || !canTakeHit) return;

        if (collision.gameObject.CompareTag("Ground")) return;

        if (!collision.gameObject.CompareTag("Obstacle") && !collision.gameObject.CompareTag("Trap")) return;

        hitCount++;
        canTakeHit = false;
        StartCoroutine(HitCooldown());

        if (hitCount == 1)
        {
            FirstDamageStage();
        }
        else if (hitCount == 2)
        {
            SecondDamageStage();
        }
        else if (hitCount >= maxHits)
        {
            Explode();
        }
    }

    void FirstDamageStage()
    {
        Debug.Log("Prvi sudar - auto se poceo paliti.");
        speed *= 0.85f;

        if (smallFireEffect != null)
            smallFireEffect.SetActive(true);
    }

    void SecondDamageStage()
    {
        Debug.Log("Drugi sudar - vatra je jaca.");
        speed *= 0.75f;

        if (smallFireEffect != null)
            smallFireEffect.SetActive(false);

        if (bigFireEffect != null)
            bigFireEffect.SetActive(true);
    }

    void Explode()
    {
        isDestroyed = true;

        Debug.Log("Treci sudar - eksplozija!");

        if (smallFireEffect != null)
            smallFireEffect.SetActive(false);

        if (bigFireEffect != null)
            bigFireEffect.SetActive(false);

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(0.4f, 0.25f));

        if (explosionSound != null)
            explosionSound.Play();

        foreach (MeshRenderer mr in renderers)
        {
            mr.enabled = false;
        }

        foreach (Collider col in allColliders)
        {
            col.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        Destroy(gameObject, 1.5f);
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        canTakeHit = true;
    }
}
