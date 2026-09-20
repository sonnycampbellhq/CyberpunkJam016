using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyLayoutPrefab;
    void OnTriggerEnter(Collider other)
    {
        for(int i=0; i<enemyLayoutPrefab.Length; i++)
        {
            Instantiate(enemyLayoutPrefab[i]);
        }
        Destroy(gameObject);
    }
}
