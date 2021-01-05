using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text winText;
    public Text defeatText;
    public Text restartText;

    public PlayerController player;
    public EnemyPackManager enemyPack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(enemyPack.CurrentNumberOfEnemy == 0)  winText.enabled = true; 


    }
}
