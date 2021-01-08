using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Transform enemyHolder;
    public float speed;

    public GameObject bullet;
    public Text winText;
    public Text restartText;
    public float fireRate = 0.95f;

   /* private int ligne = 5;
    private int colonne = 10;
    private List<Transform> list;*/

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveEnemy", 0.1f, 0.3f);
        enemyHolder = GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        //list.Clear();

        /*
        for (int i = 0; i < colonne; i++)
        {
            foreach (Transform enemy in enemyHolder)
            {


            }
        }*/
    }


    void MoveEnemy()
    {
        enemyHolder.position += Vector3.right * speed;
        Transform enemyFront;

        foreach (Transform enemy in enemyHolder)
        {
            enemyFront = EnemyFront(enemy.position.y);
            if(enemy.position.x < -10f || enemy.position.x > 10f)
            {
                speed = -speed;
                enemyHolder.position += Vector3.down * 0.5f;
                return;
            }

            //Random fire shoot
            if(Random.value > fireRate && enemyFront != null)
            {
                //Debug.Log("Enemy Y" + enemy.position.y);
                //Debug.Log("Enemy Devant" + enemyFront.position.y);

                GameObject go = Instantiate(bullet, enemyFront.position, enemyFront.rotation);
                go.name = "Bullet";
                go.GetComponent<Bullet>().bulletParent = BulletParent.Ennemy;
            }


            if(enemy.position.y <= -4)
            {

            }
        }

        if(enemyHolder.childCount == 1)
        {
            CancelInvoke();
            InvokeRepeating("MoveEnemy", 0.1f, 0.25f);
        }

        if(enemyHolder.childCount == 0)
        {
            winText.enabled = true;
            restartText.enabled = true;
        }
    }

    Transform EnemyFront(float posX)
    {
        foreach (Transform enemy in enemyHolder)
        {
            if (enemy.position.x == posX) return enemy;
        }
        return null;
    }
}
 