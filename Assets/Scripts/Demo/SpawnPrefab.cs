using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{

    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _launchSpeed;
    [SerializeField] private float scatterDistance = 0;
    [SerializeField] private KeyCode keyCode;
    [SerializeField] private int _spawnCount = 1;
    [SerializeField] private bool _randomRotation = false;
    [SerializeField] private bool _autoSpawn = false;
    [SerializeField] private float _autoSpawnRate = 1.5f;
    private float _spawnCounter = 1;

    void Update()
    {
        if (_autoSpawn)
        {
            _spawnCounter += Time.deltaTime;
        }
        if (Input.GetKeyDown(keyCode) || _spawnCounter > _autoSpawnRate)
        {
            _spawnCounter -= _autoSpawnRate;
            //
            for (int i = 0; i < _spawnCount; i++)
            { 
                GameObject gO = Instantiate(_prefab, transform.position + Random.onUnitSphere * scatterDistance, _randomRotation ? Random.rotation: transform.rotation);
                if (Mathf.Abs(_launchSpeed) > 0.001f)
                {
                    Rigidbody[] bodies = gO.GetComponentsInChildren<Rigidbody>();
                    Vector3 velocity =  (transform.forward + Random.onUnitSphere * 0.06f)* _launchSpeed;
                    foreach (Rigidbody body in bodies)
                    {
                        body.velocity = velocity;
                    } 
                     gO.transform.LookAt(transform.position + velocity, Vector3.up);
                }
            }
        }
    }
}
