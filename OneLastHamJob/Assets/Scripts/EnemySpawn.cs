using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemyLayoutPrefab;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Instantiate(enemyLayoutPrefab);
        Destroy(gameObject);
    }
}
