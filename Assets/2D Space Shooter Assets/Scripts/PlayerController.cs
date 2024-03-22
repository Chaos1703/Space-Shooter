using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float minY, maxY;
    public GameObject playerBullet;
    public Transform attackPoint;
    public float attackTime = 0.35f;
    private float currentAttackTime;
    private bool canAttack;
    private AudioSource laserSound;

    void Start()
    {
        currentAttackTime = attackTime;
        canAttack = true;
        laserSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        MovePlayer();
        Fire();
    }

    void MovePlayer(){
        if(Input.GetAxisRaw("Vertical") > 0f){
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;
            if(temp.y > maxY){
                temp.y = maxY;
            }
            transform.position = temp;
        }
        else if(Input.GetAxisRaw("Vertical") < 0f){
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            if(temp.y < minY){
                temp.y = minY;
            }
            transform.position = temp;
        }
    }

    void Fire(){
        attackTime += Time.deltaTime;
        if(Input.GetMouseButtonDown(0)){
            if(canAttack){
                canAttack = false;
                attackTime = 0f;
                Instantiate(playerBullet, attackPoint.position, Quaternion.identity);
                laserSound.Play();
            }
        }
        if(attackTime > currentAttackTime){
            canAttack = true;
            attackTime = 0f;
        }
    }
}
