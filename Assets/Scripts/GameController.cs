using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float spawnTime;
    public GameObject people;
    public List<GameObject> spawPoints = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomizarSpawn()
    {
        Instantiate(people,spawPoints[Random.Range(0,spawPoints.Count)].transform.position, Quaternion.identity);
    }

    IEnumerator StartSpawn()
    {
        RandomizarSpawn();
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(StartSpawn());
    }
}
