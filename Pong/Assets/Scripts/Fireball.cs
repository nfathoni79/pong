using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float xInitialForce = 70f;
    [SerializeField] private float yInitialForce = 70f;
    [SerializeField] private float lifeDuration = 10f;

    private PlayerControl[] players;
    private PlayerControl player1;
    private PlayerControl player2;
    private BallControl ball;
    private GameManager gameManager;
    private Rigidbody2D fireballRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<PlayerControl>();

        foreach (var player in players)
        {
            if (player.name.Equals("Player 1"))
            {
                player1 = player;
            }

            if (player.name.Equals("Player 2"))
            {
                player2 = player;
            }
        }

        ball = FindObjectOfType<BallControl>();
        gameManager = FindObjectOfType<GameManager>();

        fireballRigidbody = GetComponent<Rigidbody2D>();

        PushFireball();

        Destroy(gameObject, lifeDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerControl>())
        {
            if (other.gameObject.name.Equals("Player 1"))
            {
                player2.IncrementScore();
            }

            if (other.gameObject.name.Equals("Player 2"))
            {
                player1.IncrementScore();
            }

            if (player1.Score < gameManager.maxScore && player2.Score < gameManager.maxScore)
            {
                ball.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
            }

            Destroy(gameObject);
        }
    }

    private void PushFireball()
    {
        float xRandomInitialForce = Random.Range(-xInitialForce, xInitialForce);
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        fireballRigidbody.AddForce(new Vector2(xRandomInitialForce, yRandomInitialForce));
    }
}
