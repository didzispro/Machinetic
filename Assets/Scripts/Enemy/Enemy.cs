using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Space(5)]
    public Transform player; 
    [Space(5)]
    public GameObject bulletPrefab;
    [Space(5)]
    public GameObject platform;
    [Space(5)]
    public Transform firePoint;

    [Space(10)]
    public int zoneID;
    [Space(5)]
    public float moveSpeed = 3f;
    [Space(5)]
    public float stoppingDistance = 5f;
    [Space(5)]
    public float fireRate = 1f;

    [Space(10)]
    [Header("Sound Effects")]
    [SerializeField] private AudioClip GunSound;
    [Space(5)]
    [SerializeField] private AudioClip jumpSound;

    [Space(10)]
    [SerializeField] private float jumpForce = 60.0f;
    [Space(5)]
    [SerializeField] private float jumpTimer = 5.0f;

    private float nextFireTime;

    private EnemyCollecter enemyCollecter;
    private PlayerController playerController;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Awake()
    {
       audioSource = GetComponent<AudioSource>();
       rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemyCollecter = FindObjectOfType<EnemyCollecter>();

        StartCoroutine(JumpTimer());
    }

    void Update()
    {
        if (player == null || playerController.activeZone != zoneID || playerController.isItGameOver) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stoppingDistance)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        if (player.position.x < transform.position.x)
            transform.eulerAngles = new Vector3(0, 180, 0); 
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void FixedUpdate() 
    {
        if (player == null || playerController.activeZone != zoneID || playerController.isItGameOver) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (rb.velocity.magnitude > moveSpeed + 1f) return;

        if (distance > stoppingDistance)
        {
            float dir = (player.position.x > transform.position.x) ? 1 : -1;
            rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } 
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        audioSource.PlayOneShot(GunSound, 1.0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {  
            if (zoneID == 5 || zoneID == 6)
            {
                enemyCollecter.CollectEnemys();
                Destroy(gameObject);  
            }
            else
            {
                platform.GetComponent<Platform>().GoDown();
                enemyCollecter.CollectEnemys();
                Destroy(gameObject);
            }
        } 

        if (collision.gameObject.CompareTag("Jump"))
        {
            audioSource.PlayOneShot(jumpSound, 1.0f);
        }

    }
    IEnumerator JumpTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpTimer);

            if (playerController.activeZone == zoneID && !playerController.isItGameOver)
            {
                EnemyJump();
            }
        }
    }

    void EnemyJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
