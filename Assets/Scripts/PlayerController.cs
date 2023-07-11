using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private KeyCode shootKey;

    [Header("Stat")]
    [SerializeField] private int lifePoint = 1;
    [SerializeField] private int ammo = 1;

    private Animator anim => GetComponentInChildren<Animator>();
    private GameManager gameManager => GameManager.instance;

    private void Update()
    {
        if (gameManager.gameState == GameState.Play)
            GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(shootKey))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        if (ammo <= 0) return;

        anim.SetTrigger("Shoot");
        Instantiate(bulletPref, shootPoint.position, shootPoint.rotation);
        ammo--;
    }

    private void TakeDamage()
    {
        lifePoint--;

        if (lifePoint <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        gameManager.gameState = GameState.End;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}
