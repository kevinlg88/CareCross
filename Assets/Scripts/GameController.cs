using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float spawnTime;
    public GameObject people;
    public List<GameObject> spawPoints = new List<GameObject>();

    public GameObject gameover;
    // Start is called before the first frame update
    int numberPeople;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Tema");
        StartCoroutine(StartSpawn());
        StartCoroutine(Dificultar());
    }

    // Update is called once per frame
    void Update()
    {
        numberPeople = GameObject.FindGameObjectsWithTag("npc").Length;
        if(numberPeople > 7)
        {
            Debug.Log("GAME OVER");
            StopAllCoroutines();
            gameover.SetActive(true);
            Time.timeScale = 0f;
        }
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

    IEnumerator Dificultar()
    {
        yield return new WaitForSeconds(4f);
        spawnTime -= 0.1f;
        if(spawnTime <= 0.5)
        {
            spawnTime = 0.5f;
        }
        StartCoroutine(Dificultar());
    }
}
