using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
    Rigidbody rb; 
    public float m_acceleration;
    public float m_initVel = 0;
    public float m_finalVel = 100;
    public float m_timeInSec = 2.2f;
    float maxDist = 4;
    bool toggle = false;
    
	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            toggle = true;
        }
        if (toggle)
        {
            startSim();
        }
    }

    void startSim()
    {
        print("start");
        m_acceleration = calcAccel(m_initVel, m_finalVel, m_timeInSec);
        
        addForce();
    }
    float calcAccel(float initialVel, float finalVel, float time) {
        finalVel = initialVel + (m_acceleration * time);
        float a = (finalVel - initialVel) / time;
        print(a);
        return a;
    }
    
    float calcVel(float initalVel, float finalVel, float accel, float time)
    {
        finalVel = initalVel + (accel * Time.deltaTime);
        return finalVel;
    }
    //convert vel based on acceleration
    void Move()
    {
        if (this.transform.position.x <= maxDist)
        {
            Vector3.Lerp(transform.position, transform.position += new Vector3(-1, 0, 0), m_acceleration);
        }
    }

    void addForce()
    {
        rb.AddForce(Vector3.left * rb.mass * m_acceleration);
    }
    float kmphToMps(float kmph)
    {
        return kmph = (kmph * 5) / 18;
    }
}
