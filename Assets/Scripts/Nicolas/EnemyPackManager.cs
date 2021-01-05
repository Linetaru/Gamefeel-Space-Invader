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
    [Header("Ennemy Pack List by Columns")]
    public List<EnemyList> EnemyPackList;
    [ReadOnly] [SerializeField] private int CurrentNumberOfEnemy = 0;
    [ReadOnly] [SerializeField] private int MaxNumberOfEnemy;

    [Header("Stats Attack")]
    public float fireRate = 0.95f;
    private float fireRateTimer;

    private Vector2 screenBounds;

    [Header("Stats Movement")]
    public float baseSpeed = 0.5f;
    [ReadOnly] [SerializeField] private float speed;
    private float timerDown;
    public float timeToDown = 0.75f;
    private bool DownActivated = false;
    [ReadOnly] [SerializeField] private Phase phaseMovement;

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
        foreach (EnemyList list in EnemyPackList)
        {
            foreach (Enemy enemy in list.enemiesList)
            {
                if (enemy != null)
                    CurrentNumberOfEnemy++;
            }
        }
        MaxNumberOfEnemy = CurrentNumberOfEnemy;
        speed = baseSpeed;
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        AttackUpdate();
    }

    public void RemoveEnemy(Enemy killedEnemy)
    {
        foreach (EnemyList list in EnemyPackList)
        {
            foreach (Enemy enemy in list.enemiesList)
            {
                if (enemy == killedEnemy)
                {
                    list.enemiesList.Remove(killedEnemy);
                    CurrentNumberOfEnemy--;
                    return;
                }
            }
        }
    }

    void MovementUpdate()
    {
        if(CurrentNumberOfEnemy == MaxNumberOfEnemy)
        {
            speed = baseSpeed;
        }
        else if(CurrentNumberOfEnemy <= MaxNumberOfEnemy - 1 && CurrentNumberOfEnemy > MaxNumberOfEnemy - 4)
        {
            speed = baseSpeed + 0.2f;
        }
        else if(CurrentNumberOfEnemy <= MaxNumberOfEnemy - 5 && CurrentNumberOfEnemy > MaxNumberOfEnemy - 7)
        {
            speed = baseSpeed + 0.5f;
        }
        else
        {
            speed = baseSpeed + 1f;
        }

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
            if (timerDown >= timeToDown)
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
        if (fireRateTimer <= 0)
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
            fireRateTimer = fireRate;
        }
        else
        {
            fireRateTimer -= Time.deltaTime;
        }
    }
}
