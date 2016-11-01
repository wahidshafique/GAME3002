using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour {
    GameObject refer;
    Rigidbody rb;
    void Awake() {
        refer = GameObject.FindGameObjectWithTag("Ref");
        rb = this.GetComponent<Rigidbody>();
    }
	void Start () {
        float force = Mathf.Sqrt(2 * refer.transform.position.y * 9.8f);
        //impulse is essentially doing mass*distance/time (Force per frame)
        print(force);

        rb.AddForce(0, force, 0, ForceMode.Impulse);
    }
}
