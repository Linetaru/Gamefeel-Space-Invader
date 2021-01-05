using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Attack()
    {
        GameObject go = Instantiate(Bullet, transform.position, Quaternion.identity);
        go.name = "Bullet";
        go.GetComponent<Bullet>().bulletParent = BulletParent.Ennemy;
    }

    private void OnDestroy()
    {
        EnemyPackManager.instance.RemoveEnemy(this);
    }
}
