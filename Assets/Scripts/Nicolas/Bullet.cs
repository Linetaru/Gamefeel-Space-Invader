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
    public float speed = 0.2f;

    private Vector2 screenBounds;
    private GameObject target;

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
            //if (GameManager.instance.Feature1HeartSound)
            //{
            //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 10000, LayerMask.GetMask("Enemy"));

            //    if (hit.collider != null)
            //    {
            //        if (hit.collider.gameObject.GetComponent<Enemy>() != null && target == null)
            //        {
            //            target = hit.collider.gameObject;
            //        }
            //    }

            //    if (target != null)
            //        if (target.transform.position.y - this.gameObject.transform.position.y > 0)
            //            SoundManager.instance.SetHeartSoundPitch(target.transform.position.y - this.gameObject.transform.position.y, this.gameObject);
            //}

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
                if(GameManager.instance.Feature3ScreenShake)
                    GameManager.instance.UseScreenShake();
                other.gameObject.GetComponent<Enemy>().OnDestroyed();
                if (GameManager.instance.Feature1HeartSound)
                    SoundManager.instance.SetHeartSoundPitch();
                if(GameManager.instance.Feature8CrySound)
                    SoundManager.instance.PlayCrySound();
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

                //if (GameManager.instance.playerLife == 0)
                //{
                //    GameManager.instance.Defeat();
                //}

                Destroy(gameObject);
            }
        }
    }

}
