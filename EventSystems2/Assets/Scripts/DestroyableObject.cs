using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public GameObject[] itemDrops;
    public int dropQuantity;

    public delegate void OnEnemyDie();
    public event OnEnemyDie onEnemyDie;

    private void OnDestroy()
    {
        for (int i = 0; i < dropQuantity; i++)
        {
            GameObject itemDrop = Instantiate(itemDrops[Random.Range(0, itemDrops.Count())]);
            itemDrop.transform.position = this.transform.position;
        }
        onEnemyDie?.Invoke();
    }
}
