using UnityEngine;
using System.Collections;

public class Length : MonoBehaviour {
    public float metersOfTrack = 402;
	// Use this for initialization
	void Start () {
        MeshCollider mColl = GetComponent<MeshCollider>();
        transform.localScale = new Vector3(metersOfTrack, 1, 1);
        GameObject[] cubeArr = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < cubeArr.Length; i++) {
            cubeArr[i].transform.position = new Vector3((mColl.bounds.size.x / 2) - 8, mColl.bounds.size.y + 1, mColl.bounds.size.z - (6 + i * 3));
        }
        //transform.position = new Vector3 ((meters * GameObject.Find("Cube").transform.position.x) + meters, 0, 0);
	}

}
