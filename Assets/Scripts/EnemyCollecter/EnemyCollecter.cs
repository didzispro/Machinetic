using UnityEngine;

public class EnemyCollecter : MonoBehaviour
{
    [Tooltip("There are 11 platforms.")]
    [SerializeField] private Platform[] platforms; 
    [Space(10)]

    [SerializeField] private int enemysCollected = 0;
    
    public void CollectEnemys()
    {
        enemysCollected++;

        if (enemysCollected == 5)
        {
            platforms[3].GetComponent<Platform>().GoDown();
        }

        if (enemysCollected == 7)
        {
            platforms[4].GetComponent<Platform>().GoDown();
        }
    }
}
