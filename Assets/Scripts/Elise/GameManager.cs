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
