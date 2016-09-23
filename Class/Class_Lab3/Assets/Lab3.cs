using UnityEngine;
using System.Collections;

public class Lab3 : MonoBehaviour {
    Rigidbody m_rb;
    public float m_totalTime, m_initVel = 0.0f, m_totalDisplace;
    private float m_acceleration = 0.0f;
    private Vector3 m_netForce = Vector3.up;
    private bool m_isRunning = false;

    void Start() {
        m_rb = this.GetComponent<Rigidbody>();
        m_acceleration = (CalculateAcceleration(m_totalDisplace, m_initVel, m_totalTime));
        m_netForce *= (m_acceleration * m_rb.mass) + (9.8f * m_rb.mass);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            print("toggle");
            m_isRunning = !m_isRunning;
        } 
    }
    void FixedUpdate() {
        if (m_isRunning) {
            m_rb.AddForce(m_netForce);
        }
    }
    public float CalculateAcceleration(float displacement, float initialVelocity, float totalTime) {
        return (totalTime > Mathf.Epsilon ? ((displacement - (initialVelocity * totalTime)) * 20f) / (totalTime * totalTime) : 0.0f);
    }
}
