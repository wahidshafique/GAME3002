using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImpulseTask1 : MonoBehaviour {
    public float m_time = 3;
    public Transform m_referenceTransform;

    private Rigidbody m_rb;
    private Vector3 m_force = Vector3.zero;
    private Vector3 m_desiredDisplacement = Vector3.zero;
    private bool launched = false;
    private float triggerPoint;
    private float triggerVelocity;

    public Text idealForce;
    public Text actualForce;
    public Slider powerSlider;
    void Start() {
        powerSlider.onValueChanged.AddListener(delegate { valueChangeChecker(); });
        m_rb = GetComponent<Rigidbody>();
        triggerPoint = GameObject.FindGameObjectWithTag("TriggerPoint").transform.position.x;
    }
    void valueChangeChecker() {
        powerSlider.GetComponentInChildren<Text>().text = "Power Level: " + powerSlider.value;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !launched) {
            m_rb.AddForce((float)-powerSlider.value, 0, 0, ForceMode.Impulse);
        }
        if (Mathf.Round(this.transform.position.x) == Mathf.Round(triggerPoint) && !launched) {
            actualForce.text = "X Force Actual : " + m_rb.velocity.x.ToString();
            triggerVelocity = m_rb.velocity.x;
            m_rb.velocity = Vector3.zero;
            calculateOnTrigger();
            launched = true;
        }
    }

    void calculateOnTrigger() {//once you hit the marker, then launch
        Vector3 ourPos = GetOffsetPosition(this.transform, true);
        Vector3 refPos = GetOffsetPosition(m_referenceTransform, false);
        m_desiredDisplacement = refPos - ourPos;
        LaunchProjectile();
    }

    Vector3 GetOffsetPosition(Transform refTransform, bool isBottom) {
        return isBottom ? refTransform.position - (new Vector3(0.0f, refTransform.localScale.y, 0.0f) * 0.5f) : refTransform.position + (new Vector3(0.0f, refTransform.localScale.y, 0.0f) * 0.5f);
    }

    void LaunchProjectile() {
        m_force.y = CalculateYImpulse(m_desiredDisplacement.y, m_time);
        print("fixed Y force to reach goal was: " + m_force.y);
        idealForce.text = "X Force Ideal: " + ((m_desiredDisplacement.x / m_time) * m_rb.mass).ToString();
        m_force.x = triggerVelocity * m_rb.mass;
        //m_force.x = (m_desiredDisplacement.x / m_time) * m_rb.mass;
        m_rb.AddForce(m_force / Time.fixedDeltaTime);
        m_force = Vector3.zero;
    }

    float CalculateYImpulse(float displacement, float time) {
        float velocity = (displacement - (0.5f * Physics.gravity.y * (time * time))) / (time);
        return velocity * m_rb.mass;
    }
}