using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase{
    LeftMovement,
    DownMovement,
    RightMovement,
}

[System.Serializable]
public class EnemyList
{
    public List<Enemy> enemiesList;
}

public class EnemyPackManager : MonoBehaviour
{
    public List<EnemyList> EnemyPackList;

    public float fireRate = 0.95f;
    public float speed = 0.5f;
    private Phase phaseMovement;

    private Vector2 screenBounds;
    public float timerMax = 2;
    private float timer;

    private bool DownActivated = false;
    private float timerDown;

    public static EnemyPackManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        phaseMovement = Phase.LeftMovement;
    }

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        AttackUpdate();
    }

    void MovementUpdate()
    {
        switch (phaseMovement)
        {
            case Phase.LeftMovement:
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                break;
            case Phase.DownMovement:
                transform.Translate(new Vector2(0, -speed * Time.deltaTime));
                DownActivated = true;
                break;
            case Phase.RightMovement:
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                break;
        }

        if (!DownActivated)
        {
            foreach (EnemyList list in EnemyPackList)
            {
                if (list.enemiesList.Count != 0)
                {
                    foreach (Enemy enemy in list.enemiesList)
                    {
                        if (enemy != null)
                        {
                            if (enemy.transform.position.x <= screenBounds.x * -1)
                            {
                                phaseMovement = Phase.DownMovement;
                                return;
                            }
                            else if (enemy.transform.position.x >= screenBounds.x)
                            {
                                phaseMovement = Phase.DownMovement;
                                return;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (timerDown >= 0.75)
            {
                DownActivated = false;
                if (transform.position.x <= screenBounds.x * -1)
                    phaseMovement = Phase.RightMovement;
                else
                    phaseMovement = Phase.LeftMovement;
                timerDown = 0;
            }
            else
            {
                timerDown += Time.deltaTime;
            }
        }
    }

    void AttackUpdate()
    {
        if (timer >= timerMax)
        {
            int random = Random.Range(0, 10);
            foreach (EnemyList list in EnemyPackList)
            {
                if (list.enemiesList.Count != 0 && EnemyPackList.IndexOf(list) == random)
                {
                    foreach (Enemy enemy in list.enemiesList)
                    { 
                        if(list.enemiesList.IndexOf(enemy) == list.enemiesList.Count - 1)
                            enemy.Attack();
                    }
                    break;
                }
            }
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
