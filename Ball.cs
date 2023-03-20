using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody   m_rigidBody;
    Score       m_score;
    AudioSource m_audio;
    
    private void Start()
    {
        m_score      = GameObject.Find("Score").GetComponent<Score>();
        m_audio      = GetComponent<AudioSource>();
        m_rigidBody  = GetComponent<Rigidbody>();
    }

    public void StartGame()
    {
        Vector2 direction  = new Vector2(1, 1);
        int     rand       = Random.Range(1, 4);
        transform.position = Vector3.zero;

        if (rand == 1)
        {
            direction = new Vector2(1, 0.5f);
        }
        else if (rand == 2)
        {
            direction = new Vector2(1, 0.5f);
        }
        else if (rand == 3)
        {
            direction = new Vector2(-1, 0.5f);
        }
        else if (rand == 4)
        {
            direction = new Vector2(-1, 0.5f);
        }

        m_rigidBody.velocity = direction * 15;
    }

    public void StopGame()
    {
        transform.position   = Vector3.zero;
        m_rigidBody.velocity = Vector3.zero;
        
    }

    void OnCollisionEnter(Collision col)
    {
        float bounceAngle = GetBounceAngle(transform.position, col.transform.position, col.collider.bounds.size.y);
        m_audio.Play();
        if (col.gameObject.tag == "Player")
        {
            Vector2 direction = new Vector2(1, bounceAngle).normalized;
            m_rigidBody.velocity = direction * 15;
        }
        else if (col.gameObject.tag == "AI")
        {
            Vector2 direction = new Vector2(-1, bounceAngle).normalized;
            m_rigidBody.velocity = direction * 15F;
        }
        else if (col.gameObject.tag == "LeftGoal")
        {
            m_score.AddAIScore();

            if (m_score.IsFinish())
            {
                StopGame();
                m_score.ResetScore();
            }
            else
                StartGame();
        }
        else if (col.gameObject.tag == "RightGoal")
        {
            m_score.AddPlayerScore();

            if (m_score.IsFinish())
            {
                StopGame();
                m_score.ResetScore();
            }
            else
                StartGame();
        }
    }

    float GetBounceAngle(Vector2 ballPosition, Vector2 playerPosition, float playerSizeHeight)
    {
        return (ballPosition.y - playerPosition.y) / playerSizeHeight;
    }
}
