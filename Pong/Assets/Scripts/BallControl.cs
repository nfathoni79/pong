using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float xInitialForce = 50f;
    public float yInitialForce = 50f;

    private Rigidbody2D rigidBody2D;
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        rigidBody2D.velocity = Vector2.zero;
    }

    private void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        float forceMagnitude = new Vector2(xInitialForce, yInitialForce).magnitude;
        float xAdjustedForce = Mathf.Sqrt(forceMagnitude * forceMagnitude - yRandomInitialForce * yRandomInitialForce);

        float randomDirection = Random.Range(0, 2);

        if (randomDirection < 1f)
        {
            //rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
            rigidBody2D.AddForce(new Vector2(-xAdjustedForce, yRandomInitialForce));
        }
        else
        {
            //rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
            rigidBody2D.AddForce(new Vector2(xAdjustedForce, yRandomInitialForce));
        }
    }

    private void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
