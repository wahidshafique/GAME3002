using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
    Rigidbody[] rbs;
    public float m_accelerationPorsche;
    public float m_accelerationLaFerrari;

    public float m_initVelPorsche = 0;
    public float m_finalVelPorsche = 100;
    public float m_initVelLaFerrari = 0;
    public float m_finalVelLaFerrari = 100;

    float m_timeInSecPorsche = 2.2f;
    float m_timeInSecLaFerrari = 2.4f;

    bool toggle = false;
    GameObject[] cubes;
    private float elapsed;

    // Use this for initialization
    void Start() {
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        rbs = new Rigidbody[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
            rbs[i] = cubes[i].GetComponent<Rigidbody>();

        m_accelerationPorsche = calcAccel(kmphToMps(m_initVelPorsche), kmphToMps(m_finalVelPorsche), m_timeInSecPorsche, "Porche");
        m_accelerationLaFerrari = calcAccel(kmphToMps(m_initVelLaFerrari), kmphToMps(m_finalVelLaFerrari), m_timeInSecLaFerrari, "LaFerrari");
    }
    // Update is called once per frame

    void Update() {

        if (Input.GetKeyDown("space")) {
            toggle = !toggle;
            if (!toggle) {
                rbs[0].velocity = new Vector3(0, 0, 0);
                rbs[1].velocity = new Vector3(0, 0, 0);
            }
        }
    }

    void FixedUpdate() {
        if (toggle) {
            Sim();
            elapsed += Time.fixedDeltaTime;
            //for testing, this adds force using velocity to the TEST vehicle (it uses the Porsche as a test case)
            calcVelocity();
        };
    }

    void Sim() {

        addForce();
    }
    void calcVelocity() {
        float vel = m_initVelPorsche + m_accelerationPorsche * elapsed;
        rbs[2].velocity = new Vector3(-vel, 0, 0);
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
        print("adding force to both cars");
        rbs[0].AddForce(Vector3.left * rbs[0].mass * m_accelerationPorsche);
        rbs[1].AddForce(Vector3.left * rbs[1].mass * m_accelerationLaFerrari);
    }
    float kmphToMps(float kmph) {
        return kmph = (kmph * 5) / 18;
    }
}
