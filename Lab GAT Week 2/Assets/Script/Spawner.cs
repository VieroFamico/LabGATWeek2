using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float lifetime = 5f;


    void Start()
    {
        spawnArea = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        while (enabled)
        {
            GameObject fruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if (Random.value < bombChance)
            {
                fruit = bombPrefab;
            }

            Vector3 spawnPos = new Vector3();
            spawnPos.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            spawnPos.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            spawnPos.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion spawnRot = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject spawnedFruit = Instantiate(fruit, spawnPos, spawnRot);
            Destroy(spawnedFruit, lifetime);

            float force = Random.Range(minForce, maxForce);
            spawnedFruit.GetComponent<Rigidbody>().AddForce(spawnedFruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
        
    }
}
