using UnityEngine;
using System.Collections;

public class frozenByBoomerang : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    private Vector3 enemyPosition;
    private float time = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void freeze()
    {
        enemyPosition = enemy.transform.position;
        StartCoroutine(frozeEnemy(enemy));
    }

    IEnumerator frozeEnemy(GameObject enemyMovement)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            enemyMovement.transform.position = enemyPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}
