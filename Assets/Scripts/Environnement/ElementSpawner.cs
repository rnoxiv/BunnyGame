using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour {

    private float nextSpawn = 0;
    private float startTime;

    public AnimationCurve spawnCurve;
    public float curveLenghtInSecs = 30f;
    public float jitter = 0.25f;
    public Transform prefabToSpawn;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Time.time > nextSpawn)
        {
            if (prefabToSpawn.name == "Cactus" || prefabToSpawn.name == "mountain1" || prefabToSpawn.name == "mountain2" || prefabToSpawn.name == "mountain3")
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            else
                Instantiate(prefabToSpawn, new Vector3(transform.position.x, Random.Range(transform.position.y - 1, transform.position.y + 1), 0.0f), Quaternion.identity);
            float curvePos = (Time.time - startTime) / curveLenghtInSecs;

            if (curvePos > 1f)
            {
                curvePos = 1f;
                startTime = Time.time;
            }

            nextSpawn = Time.time + spawnCurve.Evaluate(curvePos) + Random.Range(-jitter, jitter);
                
        }

	}
}
