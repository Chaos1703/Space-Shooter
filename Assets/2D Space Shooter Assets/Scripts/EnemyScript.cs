using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 50f;
    public bool  canShoot , canRotate , canMove = true;
    public float boundX = -11f;
    public Transform attackPoint;
    public GameObject enemyBullet;
    private Animator anim;
    private AudioSource explosionSound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        if(canRotate){
            if(Random.Range(0,2) > 0){
                rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
                rotateSpeed *= -1f;
            }
        }
        if(canShoot){
            Invoke("Attack", Random.Range(1f, 3f));
        }
    }
    void Update()
    {
        Move();
        RotateEnemy();
    }

    void Move(){
        if(canMove){
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;
            if(temp.x < boundX){
                gameObject.SetActive(false);
            }
        }
    } 

    void RotateEnemy(){
        if(canRotate){
            transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime), Space.World);
        }
    }

    void Attack(){
        GameObject bullet = Instantiate(enemyBullet, attackPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().isEnemyBullet = true;
        if(canShoot){
            Invoke("Attack", Random.Range(1f, 3f));
        }
    }
    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet"){
            if(canMove){
                canMove = false;
                if(canShoot){
                    canShoot = false;
                    CancelInvoke("Attack");
                }
                Invoke("DeactivateGameObject", 0.5f);
                anim.Play("Destroyed");
                explosionSound.Play();
                Destroy(other.gameObject);
            }
        }
    }
}
