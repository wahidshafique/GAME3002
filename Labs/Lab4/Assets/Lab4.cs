using UnityEngine;
using System.Collections;

public class Lab4 : MonoBehaviour {
    [Tooltip("The time it will take to go on top of the wall. Half the time it takes to go to the target.")]
    private float m_time;
    private Vector3 m_displacement = Vector3.zero;
    public Transform m_referenceObject;
    private Rigidbody m_rb;
    private Vector3 m_force = Vector3.zero;

    private Transform marker;

    void Start () {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;
        m_rb = GetComponent<Rigidbody>();
        m_displacement = m_referenceObject.position - this.transform.position;
        m_time = m_referenceObject.position.y / 2;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        m_force.y = CalculateYImpulse(m_displacement.y, m_time);
        m_force.x = (m_displacement.x / m_time) * m_rb.mass;
        m_rb.AddForce(m_force / Time.fixedDeltaTime);
        m_force = Vector3.zero;
    }

    float CalculateYImpulse(float displacement, float time)
    {
        float velocity = time > Mathf.Epsilon  ? (displacement - 0.5f * (Physics.gravity.y) * (time * time) + displacement) / (time) : 0.0f;
        //since we are starting at rest, the difference in velocity is the velocity we calculated
        return velocity * m_rb.mass;
    }
}
