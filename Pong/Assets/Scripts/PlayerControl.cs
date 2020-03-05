using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public float speed = 10f;
    public float yBoundary = 9f;

    private Rigidbody2D rigidBody2D;
    private int score;
    private ContactPoint2D lastContactPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidBody2D.velocity;

        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0f;
        }

        rigidBody2D.velocity = velocity;

        Vector2 position = transform.position;

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        transform.position = position;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get { return score; }
    }

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = other.GetContact(0);
        }
    }

    public void Expand(float duration)
    {
        transform.localScale = new Vector3(1f, 2f, 1f);
        StartCoroutine(WaitToShrink(duration));
    }

    private void Shrink()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private IEnumerator WaitToShrink(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shrink();
    }
}
