using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float effectDuration = 15f;

    private PlayerControl[] players;
    private PlayerControl player1;
    private PlayerControl player2;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BallControl>())
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0f)
            {
                player1.Expand(effectDuration);
            }
            else
            {
                player2.Expand(effectDuration);
            }

            Destroy(gameObject);
        }
    }
}
