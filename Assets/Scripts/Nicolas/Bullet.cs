using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletParent{
    None,
    Player,
    Ennemy,
}

public class Bullet : MonoBehaviour
{
    public BulletParent bulletParent;
    public float speed = 2f;

    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletParent == BulletParent.Player)
        {
            transform.Translate(new Vector3(0, 5 * Time.deltaTime * speed, 0));
            if (transform.position.y > screenBounds.y)
                Destroy(gameObject, 0.1f);
        }
        else if (bulletParent == BulletParent.Ennemy)
        {
            transform.Translate(new Vector3(0, -5 * Time.deltaTime * speed, 0));
            if (transform.position.y < screenBounds.y * -1)
                Destroy(gameObject, 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            if (bulletParent == BulletParent.Player)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer == 10)
        {
            if (bulletParent == BulletParent.Ennemy)
            {
                GameManager.instance.playerLife--;
                Destroy(other.gameObject);
                GameManager.instance.UpdateLife();

                if (GameManager.instance.playerLife == 0)
                {
                    GameManager.instance.Defeat();
                }

                Destroy(gameObject);
            }
        }
    }

}
