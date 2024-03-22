using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f , deactivateTimer = 3f;
    [HideInInspector] public bool isEnemyBullet;
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
        if(isEnemyBullet){
            speed *= -1f;
        }
    }
    void Update()
    {
        Move();
    }

    void Move(){
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }

    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Bullet" || target.tag == "Enemy"){
            gameObject.SetActive(false);
        }
    }
}
