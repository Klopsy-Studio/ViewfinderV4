using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Transform m_ballTransform;
    Rigidbody m_ballRigid;

    void Start()
    {
        m_ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
        m_ballRigid     = m_ballTransform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (m_ballRigid.velocity.x > 0)
        {
            if (m_ballTransform.position.y < transform.position.y - 1F)
            {
                transform.Translate(Vector3.down * 10 * Time.deltaTime);
            }
            else if (m_ballTransform.position.y > transform.position.y + 1F)
            {
                transform.Translate(Vector3.up * 10 * Time.deltaTime);
            }
        }

        if (transform.position.y > 13)
        {
            transform.position = new Vector3(transform.position.x, 13, 0);
        }
        else if (transform.position.y < -13)
        {
            transform.position = new Vector3(transform.position.x, -13, 0);
        }
    }
}
