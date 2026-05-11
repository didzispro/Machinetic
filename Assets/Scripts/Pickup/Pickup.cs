using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Space(5)]
    public Transform holdPoint;
    [Space(10)]

    [Header("Sound Effects")]
    [SerializeField] private AudioClip PickupSound;

    private GameObject gunInRange;

    private AudioSource AudioSource;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gunInRange != null)
        {
            AudioSource.PlayOneShot(PickupSound, 1.0f);
            gunInRange.transform.SetParent(holdPoint);
            
            gunInRange.transform.localPosition = Vector3.zero; 
            gunInRange.transform.localRotation = Quaternion.identity;

            GetComponent<PlayerController>().hasGun = true; 

            gunInRange.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun"))
        {
            gunInRange = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Gun"))
        {
            gunInRange = null;
        }
    }
}