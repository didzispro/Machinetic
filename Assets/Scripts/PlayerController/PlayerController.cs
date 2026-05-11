using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space(5)]
    [Tooltip("There are 7 canvas.")]
    [SerializeField] private GameObject[] canvas;

    [Space(5)]
    [Header("Cameras")]
    [Tooltip("There are 7 cameras.")]
    [SerializeField] private GameObject[] cameras;

    private FadeText fadeText;

    private int currentCameraIndex = -1;

    [Space(10)]
    [SerializeField] private GameObject bulletPrefab;
    [Space(5)]
    [SerializeField] private Transform firePoint;
    [Space(10)]
    [SerializeField] private GameObject CanvasTextGameOver;
    [Space(5)]
    [SerializeField] private GameObject CanvasTextWin;

    [SerializeField] private CanvasGroup[] canvasGroups;

    [Header("Sound Effects")]
    [Space(5)]
    [SerializeField] private AudioClip GunSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip jumpSound;

    [Header("Audio Sources")]
    [Space(5)]
    [SerializeField] private AudioSource SoundEffect;
    [Space(10)]
    public bool hasGun = false;
    [Space(5)]
    public int activeZone;
    [Space(5)]
    public bool isItGameOver = false;

    private float moveSpeed = 10.0f;
    private float jumpForce = 11.0f;

    private bool jumpPressed = false;
    private bool isOnGround = false;
    
    private AudioSource audioSource;
    
    Rigidbody2D rb;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        fadeText = FindObjectOfType<FadeText>();

        ActivateCamera(0); 
        activeZone = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isItGameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
        {
            // Normal Raw movement
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            Flip(horizontalInput);
        }
        else
        {
            // ONLY snap to 0 if we aren't currently flying from a push
            // We check if our velocity is within a "normal" range (moveSpeed + a little extra)
            if (Mathf.Abs(rb.velocity.x) < moveSpeed + 0.5f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            // If velocity is HIGHER than moveSpeed, we do NOTHING.
            // This allows the physics engine to handle the push naturally.
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isItGameOver)
        {
            jumpPressed = true;
            isOnGround = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && hasGun && !isItGameOver)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (jumpPressed && !isItGameOver)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpPressed = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Lava") && !isItGameOver)
        {
            SoundEffect.PlayOneShot(deathSound, 0.7f);
            CanvasTextGameOver.SetActive(true);
            isItGameOver = true;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Jump"))
        {
            audioSource.PlayOneShot(jumpSound, 1.0f);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isOnGround = true;
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        audioSource.PlayOneShot(GunSound, 1.0f);
    }

    public void Flip(float moveX)
    {
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        CameraSwitch(collision);
        Trigger(collision);
    }

    private void CameraSwitch(Collider2D collision)
    {
        if (collision.CompareTag("ToCam1"))
        {
            ActivateCamera(0);
        }
        else if (collision.CompareTag("ToCam2"))
        {
            ActivateCamera(1);
            activeZone = 2;   
        }
        else if (collision.CompareTag("ToCam3"))
        {
            ActivateCamera(2);
            activeZone = 3;
        }
        else if (collision.CompareTag("ToCam4"))
        {
            ActivateCamera(3);
            activeZone = 4;
        }
        else if (collision.CompareTag("ToCam5"))
        {
            ActivateCamera(4);
            activeZone = 5;
        }
        else if (collision.CompareTag("ToCam6"))
        {
            ActivateCamera(5);
            activeZone = 6;
        }
        else if (collision.CompareTag("ToCam7"))
        {
            ActivateCamera(6);
            activeZone = 7;
        }
    }
    
    private void Trigger(Collider2D collision)
    {
        if (collision.CompareTag("Trigger") && !isItGameOver)
        {
            audioSource.PlayOneShot(winSound, 1.0f);
            CanvasTextWin.SetActive(true);
            isItGameOver = true;
        }
    }

    void ActivateCamera(int index)
    {
        if (index == currentCameraIndex)
        {
            return; 
        }

        currentCameraIndex = index;

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
            canvas[i].SetActive(false);
        }

        cameras[index].SetActive(true);
        canvas[index].SetActive(true);

        FadeText currentFade = canvas[index].GetComponent<FadeText>();

        if (currentFade != null)
        {
            currentFade.RestartFade();
        }
    }
}
