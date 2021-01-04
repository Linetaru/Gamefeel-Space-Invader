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
            transform.Translate(new Vector3(0, 5 * Time.deltaTime, 0));
            if (transform.position.y > screenBounds.y)
                Destroy(gameObject, 0.1f);
        }
        else if(bulletParent == BulletParent.Ennemy)
        {
            transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));
            if (transform.position.y < screenBounds.y * -1)
                Destroy(gameObject, 0.1f);
        }
    }
}
