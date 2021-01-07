using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Bullet;

    public Sprite classicSprite;
    public Sprite emojiSprite;
    public Sprite deadSprite;

    public GameObject deadEnemy;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = classicSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.Feature4EmojiSprite && GetComponent<SpriteRenderer>().sprite != emojiSprite)
        {
            GetComponent<SpriteRenderer>().sprite = emojiSprite;
        }
        else if(!GameManager.instance.Feature4EmojiSprite && GetComponent<SpriteRenderer>().sprite == emojiSprite)
        {
            GetComponent<SpriteRenderer>().sprite = classicSprite;
        }
    }

    public void Attack()
    {
        GameObject go = Instantiate(Bullet, transform.position, Quaternion.identity);
        go.name = "Bullet";
        go.GetComponent<Bullet>().bulletParent = BulletParent.Ennemy;
    }

    public void OnDestroyed()
    {
        EnemyPackManager.instance.RemoveEnemy(this);
        if (GameManager.instance.Feature4EmojiSprite)
        {
            GameObject go = Instantiate(deadEnemy, transform.position, Quaternion.identity);
            GameManager.instance.deadEmoji.Add(go);
        }
    }
}
