using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;

    public Transform puntoA;
    public Transform puntoB;

    private Transform objetivo;
    private Animator animator;
    private Collider2D enemyCollider;

    private bool muerto = false;

    private int vida = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();

        objetivo = puntoB;
    }

    void Update()
    {
        if (muerto)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            objetivo.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, objetivo.position) < 0.1f)
        {
            if (objetivo == puntoA)
            {
                objetivo = puntoB;
            }
            else
            {
                objetivo = puntoA;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (muerto)
        {
            return;
        }

        if (collision.transform.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.RecibirDaño();
            }
        }
    }

    public void Morir()
    {
        if (muerto)
            return;

        muerto = true;

        enemyCollider.enabled = false;
        animator.SetTrigger("Death");
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }

    public void RecibirGolpe()
    {
        if (muerto)
        {
            return;
        }

        vida--;

        if (vida <= 0)
        {
            Morir();
        }
    }
}