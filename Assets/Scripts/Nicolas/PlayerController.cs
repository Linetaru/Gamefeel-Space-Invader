﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bullet;
    private GameObject currentBullet;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.GetIsInRespawn())
        {
            Movement();
            Attack();
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBullet == null)
        {
            currentBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            currentBullet.name = "Bullet";
            currentBullet.GetComponent<Bullet>().bulletParent = BulletParent.Player;
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        }

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
