using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip endLevelSound;

    public float speed = 6f;
    public float jumpForce = 6f;

    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    public Transform attackPoint;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayer;

    private Rigidbody2D rb2D;
    public Animator anim;

    private float move;
    private bool isGrounded;
    private bool muerto = false;

    // Recurso del que crearemos cada copia del proyectil.
    public GameObject projectilePrefab;

    // Objeto hijo del Player que indica dónde debe aparecer.
    public Transform throwPoint;

    // El sprite original mira a la derecha.
    private float ultimaDireccion = 1f;

    public UIManager uiManager;

    public int saludActual = 3;
    public int saludMaxima = 3;

    void Start()
    {
        AudioSource.PlayClipAtPoint(endLevelSound, transform.position);
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        saludActual = saludMaxima;
    }

    void Update()
    {
        if (muerto)
            return;

        if (Input.GetKeyDown(KeyCode.C))
        {
                LanzarObjeto();
        }

        move = Input.GetAxisRaw("Horizontal");

        rb2D.linearVelocity = new Vector2(
            move * speed,
            rb2D.linearVelocity.y
        );

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            rb2D.linearVelocity = new Vector2(
                rb2D.linearVelocity.x,
                jumpForce
            );
            isGrounded = false;
        }

        anim.SetFloat("Speed", Mathf.Abs(move));
        anim.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        anim.SetBool("IsGrounded", isGrounded);

        if (move != 0)
        {
            // Conservamos la última dirección cuando el jugador se detiene.
            ultimaDireccion = Mathf.Sign(move);
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("attack");
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
            Collider2D enemyCollider = Physics2D.OverlapCircle(
                attackPoint.position,
                attackRadius,
                enemyLayer
            );

            if (enemyCollider != null)
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.RecibirGolpe();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        ) != null && rb2D.linearVelocity.y <= 0.1f;
    }

    public void Morir()
    {
        if (muerto)
        {
            return;
        }

        muerto = true;
        AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
        rb2D.linearVelocity = Vector2.zero;
        anim.SetFloat("Speed", 0f);
        anim.SetFloat("VerticalVelocity", 0f);
        anim.ResetTrigger("attack");
        anim.SetTrigger("Death");
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(
                attackPoint.position,
                attackRadius
            );
        }
    }

    private void LanzarObjeto()
    {
        // Verificamos si tienes un UIManager asignado y si te quedan disparos
        if (uiManager.GastarDisparo())
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            // Creamos una copia del prefab en la posición de ThrowPoint.
            GameObject projectile = Instantiate(
                projectilePrefab,
                throwPoint.position,
                Quaternion.identity
            );

            // Obtenemos el script de esa copia y le indicamos la dirección.
            projectile.GetComponent<Projectile>().Lanzar(ultimaDireccion);
            
        }
    }

    public void RecibirDaño()
    {
        if (muerto) return;

        saludActual--;

        // --- AÑADE ESTA LÍNEA AQUí ---
        if (uiManager != null)
        {
            uiManager.UpdateHealth(saludActual);
        }
        // -----------------------------

        if (saludActual <= 0)
        {
            saludActual = 0;
            Morir(); // Solo muere cuando se agotan los corazones
        } else
        {
            anim.SetTrigger("Hit");
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
    }


}