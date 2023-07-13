using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] balls;

    private void Start()
    {
        StartCoroutine(spawnBalls());
    }

    public IEnumerator spawnBalls()
    {
        while (true)
        {
            float randomTime = Random.Range(2f, 5f);
            int randomNum = Random.Range(5, 10);

            for (int i = 0; i < randomNum; i++)
            {
                int randomType = Random.Range(0, balls.Length - 1);

                Instantiate(balls[randomType], transform.position, transform.rotation, null);
            }

            yield return new WaitForSeconds(randomTime);
        }
    }
}
