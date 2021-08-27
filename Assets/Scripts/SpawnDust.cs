using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDust : MonoBehaviour
{
    public GameObject dustParticle;
    public string objectTag;

    void OnCollisionEnter(Collision col) {
        Debug.Log("OnCollisionEnter. Tag=" + col.gameObject.tag);

        if (col.gameObject.tag == objectTag) {
            //Instantiate a particle system
            var particle = Instantiate(dustParticle, col.gameObject.transform.position, dustParticle.transform.rotation);
            //And then destroy it after 3 seconds
            Destroy(particle, 1);
        }
    }
}
