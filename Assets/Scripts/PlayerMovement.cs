using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Se establecen los objetos necesarios para el movimiento del jugador, como el controlador de personaje y
    //las acciones de entrada con el nuevo sistema de entrada de Unity.
    private PlayerInputActions inputActions;
    private CharacterController controller;
    private PlayerStats stats; //Script con las estadisticas del jugador, para editarlas
    private Vector2 moveInput;
    private Vector3 velocity;
    //Se definen variables para la velocidad, gravedad y altura de salto.
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    //Se define un booleano para verificar si el jugador está en el suelo.
    bool isGrounded;
    [Header("Dash Settings")]
    private bool isDashing;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private float lastDashTime;
    

    // Variables para PlayerAnimationManager

    public PlayerInputActions inputActionsForAnimator { get; private set; }

    //En el método Awake, se inicializan las acciones de entrada y se obtiene el componente CharacterController.
    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActionsForAnimator = inputActions;
        controller = GetComponent<CharacterController>();
        // llamada para el script de estadisticas
        stats = GetComponent<PlayerStats>();
    }
    //En el método OnEnable, se habilitan las acciones de entrada y se suscriben a los eventos de movimiento y salto.
    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Dash.started += ctx => OnDash();
    }
    //En el método OnDisable, se deshabilitan las acciones de entrada para evitar que sigan
    //recibiendo acciones cuando el objeto esté desactivado.
    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        Move();
        ApplyGravity();
    }
    //Permite que se mueva sin parecer una papa tiesa, haciendo que el personaje gire suavemente hacia la dirección del movimiento.
    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDirection = camForward * move.z + camRight * move.x;

        if (moveDirection != Vector3.zero)
        {
            //Se mueve hacia la dirección del movimiento solo si el jugador está moviéndose hacia adelante o hacia los lados, 
            //evitando que gire al retroceder y cause interacciones raras con la cámara.
            if (moveInput.y >= 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }
        }
        speed = stats.CurrentMoveSpeed;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
    //Aplica la gravedad al jugador, asegurándose de que el jugador se mantenga en el suelo y pueda saltar correctamente.
    void ApplyGravity()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    //Evita que el jugador salte infinitamente sin tocar el suelo,
    //verifica si el jugador esta en el suelo (evita saltar sobre estructuras como ventanas o puertas)
    void Jump()
    {
        //Se usa un RaycastHit para obtener información sobre el objeto que está debajo del jugador
        //asegurándose de que solo pueda saltar si está 1.5 unidades tocando el suelo (Ground) y no otras superficies.
        RaycastHit hit;
        if (controller.isGrounded && Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f) && hit.collider.CompareTag("Ground"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    // Control para limitar los dash + cooldown
    void OnDash()
    {
        if (!isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(PerformDash());
        }
    }
    // aplicacion del dash al movimiento del jugador
    private IEnumerator PerformDash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        float dashForce = stats != null ? stats.CurrentDashForce : 15f;

        Vector3 inputDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        Vector3 dashDirection = inputDir != Vector3.zero ? inputDir : transform.forward;

        float timer = 0f;
        while (timer < dashDuration)
        {
            // Movimiento constante ignorando la velocidad normal y la gravedad
            controller.Move(dashDirection * dashForce * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null; // Hacemos una espera al siguiente frame
        }

        isDashing = false;
    }
}
