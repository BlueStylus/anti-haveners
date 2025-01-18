using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    public GameObject chicken;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnChicken(spawnTime, chicken));
    }

    private IEnumerator spawnChicken(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newChicken = Instantiate(enemy,
            new Vector2(97.54816f, -2.202f), Quaternion.identity);

        StartCoroutine(spawnChicken(interval, enemy));
    }
}
