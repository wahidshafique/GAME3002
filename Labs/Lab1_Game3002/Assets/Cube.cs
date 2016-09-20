using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
    Rigidbody[] rbs;
    public float m_accelerationPorsche;
    public float m_accelerationLaFerrari;

    public float m_initVel = 0;
    public float m_finalVel = 100;

    float m_timeInSecPorsche = 2.2f;
    float m_timeInSecLaFerrari = 2.4f;

    float distanceTravelledPorsche = 0;
    float distanceTravelledLaFerrari = 0;

    Vector3 previous;

    bool toggle = false;
    GameObject[] cubes;

    // Use this for initialization
    void Start() {
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        rbs = new Rigidbody[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
            rbs[i] = cubes[i].GetComponent<Rigidbody>();
    }
    // Update is called once per frame

    void Update() {
        
        if (Input.GetKeyDown("space")) {
            
            toggle = true;
        }
    }

    void FixedUpdate() {

        if (toggle) {
            Sim();
        };
    }

    void Sim() {
        m_accelerationPorsche = calcAccel(m_initVel, kmphToMps(m_finalVel), m_timeInSecPorsche, "Porche");
        m_accelerationLaFerrari = calcAccel(m_initVel, kmphToMps(m_finalVel), m_timeInSecLaFerrari, "LaFerrari");
        addForce();
    }
    void calcVelocity(GameObject vehicle) {
        float velocity = ((vehicle.transform.position - previous).magnitude) / Time.fixedDeltaTime;
        previous = vehicle.transform.position;
        print("velocity of Porche is " + velocity);
        //now apply it to test vehicle [2]
        rbs[2].velocity = new Vector3(-velocity, 0, 0);
    }
    float calcAccel(float initialVel, float finalVel, float time, string carType) {
        if (carType == "Porche")
            time = m_timeInSecPorsche;
        else time = m_timeInSecLaFerrari;
        float a = (finalVel - initialVel) / time;
        print(a);
        return a;
    }

    void addForce() {
        //if (distanceTravelledLaFerrari >= 2404 && distanceTravelledPorsche >= 2404) {
        //    toggle = false;
        //    rbs[0].velocity = new Vector3(0, 0, 0);
        //    rbs[1].velocity = new Vector3(0, 0, 0);
        //    print("sim over");
        //    return;
        //}
        //so this adds force based on the accel formula

        print("adding force to both cars");
        rbs[0].AddForce(Vector3.left * rbs[0].mass * m_accelerationPorsche);
        rbs[1].AddForce(Vector3.left * rbs[1].mass * m_accelerationLaFerrari);

        //for testing, this adds force using velocity to the TEST vehicle (it uses the Porsche as a test case)
        calcVelocity(rbs[0].gameObject);
        previous = rbs[0].transform.position;
    }
    float kmphToMps(float kmph) {
        return kmph = (kmph * 5) / 18;
    }
}
