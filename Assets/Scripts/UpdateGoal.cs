using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGoal : MonoBehaviour
{

    public GameObject ball;
    public GameObject BallPrefab;
    public Transform ballStartTransform;

    public float waitTime = 3;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ball") {
            Debug.Log("Goal");
            
            Destroy(ball);

            StartCoroutine(Delay());

            ball = (GameObject) Instantiate(BallPrefab, ballStartTransform.position, Quaternion.identity);
        }
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(waitTime);
    }
}
