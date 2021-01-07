using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Text Interaction")]
    public Text winText;
    public Text defeatText;
    public Text restartText;
    public Text lifeText;

    [Header("Player Information")]
    public PlayerController player;
    public int playerLife = 3;
    public GameObject playerPrefab;
    [ReadOnly] public Vector3 playerPosition;

    [ReadOnly] public bool IsGameOver;
    private bool IsVictory;
    private bool IsInRespawn;

    [ReadOnly] public bool Feature1HeartSound;
    [ReadOnly] public bool Feature2;
    [ReadOnly] public bool Feature3;
    [ReadOnly] public bool Feature4;
    [ReadOnly] public bool Feature5;
    [ReadOnly] public bool Feature6;
    [ReadOnly] public bool Feature7;
    [ReadOnly] public bool Feature8;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        winText.enabled = false;
        defeatText.enabled = false;
        restartText.enabled = false;
        playerPosition = player.transform.position;
        lifeText.text = "Life : " + playerLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && (IsGameOver || IsVictory))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Q))
            Feature1HeartSound = !Feature1HeartSound;
        if (Input.GetKeyDown(KeyCode.S))
            Feature2 = !Feature2;
        if (Input.GetKeyDown(KeyCode.D))
            Feature3 = !Feature3;
        if (Input.GetKeyDown(KeyCode.F))
            Feature4 = !Feature4;
        if (Input.GetKeyDown(KeyCode.G))
            Feature5 = !Feature5;
        if (Input.GetKeyDown(KeyCode.H))
            Feature6 = !Feature6;
        if (Input.GetKeyDown(KeyCode.J))
            Feature7 = !Feature7;
        if (Input.GetKeyDown(KeyCode.K))
            Feature8 = !Feature8;
    }

    public bool GetIsInRespawn()
    {
        return IsInRespawn;
    }

    public void RespawnPlayer()
    {
        IsInRespawn = true;
        GameObject go = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        go.name = playerPrefab.name;
        player = go.GetComponent<PlayerController>();
        go.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f)
            .OnComplete(
            () => go.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f).OnComplete(
            () => go.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).OnComplete(
            () => go.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f).OnComplete(
            () => IsInRespawn = false)))
            );
    }

    public void UpdateLife()
    {
        lifeText.text = "Life : " + playerLife;
        if(playerLife > 0)
        {
            RespawnPlayer();
        }
    }

    public void Victory()
    {
        IsVictory = true;
        winText.enabled = true;
        restartText.enabled = true;
    }

    public void Defeat()
    {
        IsGameOver = true;
        defeatText.enabled = true;
        restartText.enabled = true;
    }
}
