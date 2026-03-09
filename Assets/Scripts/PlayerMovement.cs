using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Se establecen los objetos necesarios para el movimiento del jugador, como el controlador de personaje y
    //las acciones de entrada con el nuevo sistema de entrada de Unity.
    private PlayerInputActions inputActions;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    //Se definen variables para la velocidad, gravedad y altura de salto.
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    //Se define un booleano para verificar si el jugador está en el suelo.
    bool isGrounded;
    //En el método Awake, se inicializan las acciones de entrada y se obtiene el componente CharacterController.
    void Awake()
    {
        inputActions = new PlayerInputActions();
        controller = GetComponent<CharacterController>();
    }
    //En el método OnEnable, se habilitan las acciones de entrada y se suscriben a los eventos de movimiento y salto.
    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => Jump();
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
        // Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        // controller.Move(move * speed * Time.deltaTime);
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
        controller.Move(move * speed * Time.deltaTime);
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
    //Evita que el jugador salte infinitamente sin tocar el suelo.
    void Jump()
    {
        if (controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
