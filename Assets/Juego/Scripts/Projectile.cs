using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;

    private Rigidbody2D rb2D;
    private bool consumido = false;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    public void Lanzar(float direccion)
    {
        rb2D.linearVelocity = new Vector2(direccion * speed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (consumido)
        {
            return;
        }

        Enemy enemigo = collision.GetComponent<Enemy>();

        if (enemigo != null)
        {
            consumido = true;
            enemigo.RecibirGolpe();
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            consumido = true;
            Destroy(gameObject);
        }
    }
}