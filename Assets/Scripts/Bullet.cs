using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameManager gameManager => GameManager.instance;

    private void Update()
    {
        if (gameManager.gameState == GameState.End)
        {
            transform.Translate(Vector3.zero);
            return;
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
