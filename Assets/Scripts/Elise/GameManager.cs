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

    [Header("Feature Boolean")]
    [ReadOnly] public bool Feature1HeartSound;
    [ReadOnly] public bool Feature2SublimImage;
    [ReadOnly] public bool Feature3ScreenShake;
    public ScreenShake screenShakeScript;
    [ReadOnly] public bool Feature4EmojiSprite;
    public List<GameObject> deadEmoji;
    [ReadOnly] public bool Feature5BackgroundEffect;
    public GameObject backgroundEffect;
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
        backgroundEffect.SetActive(false);
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
            Feature2SublimImage = !Feature2SublimImage;
        if (Input.GetKeyDown(KeyCode.D))
            Feature3ScreenShake = !Feature3ScreenShake;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Feature4EmojiSprite = !Feature4EmojiSprite;
            if (deadEmoji.Count != 0)
                foreach (GameObject go in deadEmoji)
                {
                    go.SetActive(Feature4EmojiSprite);
                }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Feature5BackgroundEffect = !Feature5BackgroundEffect;
            backgroundEffect.SetActive(Feature5BackgroundEffect);
        }
        if (Input.GetKeyDown(KeyCode.H))
            Feature6 = !Feature6;
        if (Input.GetKeyDown(KeyCode.J))
            Feature7 = !Feature7;
        if (Input.GetKeyDown(KeyCode.K))
            Feature8 = !Feature8;


    }

    public void UseScreenShake()
    {
        screenShakeScript.TriggerShake();
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
