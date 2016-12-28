using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public FlexibleBall flexibleBallPrefab;
    public List<FlexibleBall> flexibleBalls;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-(15.0f+i), (15.0f + i)),
                Random.Range(-5, 5),
                Random.Range(-(15.0f + i), (15.0f + i)));
            var randomRotation = Quaternion.Euler(
                Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360));
            flexibleBalls.Add(Instantiate(flexibleBallPrefab, randomPosition, randomRotation));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // FixedUpdate is called uniformly at fixed points in time (for physical forces)
    void FixedUpdate()
    {
        float centerOfMassX = 0f;
        float centerOfMassY = 0f;
        float centerOfMassZ = 0f;
        foreach (FlexibleBall flexibleBall in flexibleBalls)
        {
            centerOfMassX += flexibleBall.transform.position.x;
            centerOfMassY += flexibleBall.transform.position.y;
            centerOfMassZ += flexibleBall.transform.position.z;
        }
        centerOfMassX = centerOfMassX / (flexibleBalls.Count);
        centerOfMassY = centerOfMassY / (flexibleBalls.Count);
        centerOfMassZ = centerOfMassZ / (flexibleBalls.Count);
        for (int i = 0; i < flexibleBalls.Count; i++)
        {
            Rigidbody rb = flexibleBalls[i].GetComponent<Rigidbody>();
            Vector3 force = new Vector3(
                ((centerOfMassX - flexibleBalls[i].transform.position.x) / 10),
                ((centerOfMassY - flexibleBalls[i].transform.position.y) / 10),
                ((centerOfMassZ - flexibleBalls[i].transform.position.z) / 10));
            rb.AddForce(force);
        }
        GameObject emptyObjectCenterMass = GameObject.Find("emptyObjectCenterMass");
        emptyObjectCenterMass.transform.position = new Vector3(centerOfMassX, centerOfMassY, centerOfMassZ);
    }
}
