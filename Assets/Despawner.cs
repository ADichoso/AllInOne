using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(removeBall(collision.collider.gameObject));
    }

    public IEnumerator removeBall(GameObject ball)
    {
        float randomTime = Random.Range(2f, 5f);

        yield return new WaitForSeconds(randomTime);

        Destroy(ball);
    }
}
