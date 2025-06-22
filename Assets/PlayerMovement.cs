//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;


//public class PlayerMovement : MonoBehaviour
//{
//    public float moveSpeed = 2f;
//    public float runSpeed = 5f;
//    public float jumpHeight = 2f;
//    public LayerMask groundLayer;

//    public TextMeshProUGUI pointsText;
//    public TextMeshProUGUI healthText;
//    public Light[] allLights;

//    public float gameTime = 60f; //
//    public TextMeshProUGUI timerText;
//    public GameObject gameOverPanel;
//    public TextMeshProUGUI finalPointsText;

//    private bool isGameOver = false;

//    public int maxHealth = 100;
//    public int currentHealth;
//    public int currentPoints;

//    private CharacterController controller;
//    private Vector3 velocity;
//    private bool isGrounded;
//    private float gravity = -9.81f;
//    private Animator animator;

//    void Start()
//    {
//        enabled = false;
//        animator = GetComponent<Animator>();
//        controller = GetComponent<CharacterController>();

//        currentHealth = maxHealth;
//        currentPoints = 0;

//        TurnOffAllLights(); // Pastikan semua lampu mati saat start
//        UpdateUI();
//    }

//    void Update()
//    {
//        if (isGameOver)
//            return;

//        GroundCheck();
//        Move();

//        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//        {
//            Jump();
//        }

//        ApplyGravity();

//        UpdateTimer(); ;
//    }

//    void Move()
//    {
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");

//        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

//        // Ambil arah kamera
//        Transform cam = Camera.main.transform;

//        // Buat vektor arah berdasarkan kamera
//        Vector3 forward = cam.forward;
//        Vector3 right = cam.right;

//        // Pastikan hanya di bidang XZ (tidak naik/turun)
//        forward.y = 0f;
//        right.y = 0f;
//        forward.Normalize();
//        right.Normalize();

//        Vector3 moveDirection = forward * moveZ + right * moveX;

//        // Gerakkan player
//        controller.Move(moveDirection * speed * Time.deltaTime);

//        // Animasi
//        animator.SetFloat("Speed", moveDirection.magnitude);

//        // Rotasi menghadap arah gerak
//        if (moveDirection != Vector3.zero)
//        {
//            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
//            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
//        }
//    }

//    void Jump()
//    {
//        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
//    }

//    void ApplyGravity()
//    {
//        velocity.y += gravity * Time.deltaTime;
//        controller.Move(velocity * Time.deltaTime);
//    }

//    void GroundCheck()
//    {
//        isGrounded = controller.isGrounded;
//        if (isGrounded && velocity.y < 0)
//        {
//            velocity.y = -2f;
//        }
//    }

//    public void AddPoints(int amount)
//    {
//        currentPoints += amount;
//        UpdateUI();
//    }

//    public void AddHealth(int amount)
//    {
//        currentHealth += amount;
//        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
//        UpdateUI();
//    }

//    public void TurnOnAllLights()
//    {
//        foreach (Light light in allLights)
//        {
//            light.enabled = true;
//        }
//    }

//    public void TurnOffAllLights()
//    {
//        foreach (Light light in allLights)
//        {
//            light.enabled = false;
//        }
//    }

//    private void UpdateUI()
//    {
//        if (pointsText != null)
//            pointsText.text = "Points: " + currentPoints;

//        if (healthText != null)
//            healthText.text = "Health: " + currentHealth;
//    }
//    void UpdateTimer()
//    {
//        gameTime -= Time.deltaTime;
//        if (gameTime <= 0)
//        {
//            gameTime = 0;
//            EndGame();
//        }

//        if (timerText != null)
//        {
//            timerText.text = "Time: " + Mathf.CeilToInt(gameTime).ToString();
//        }
//    }
//    void EndGame()
//    {
//        isGameOver = true;

//        // Tampilkan Game Over UI
//        if (gameOverPanel != null)
//            gameOverPanel.SetActive(true);

//        if (finalPointsText != null)
//            finalPointsText.text = "Final Points: " + currentPoints;
//    }
//    public void Retry()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }


//}
