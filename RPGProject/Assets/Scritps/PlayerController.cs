using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;
    public int gold;
    public int curHp;
    public int maxHp;
    public bool dead;

    [Header("Attack")]
    public int damage;
    public float attackRange;
    public float attackRate;
    public float lastAttackTime;

    [Header("Components")]
    public Rigidbody2D rig;
    public Player photonPlayer;
    public Animator weaponAnim;
    public HeaderInfo headerInfo;

    // local player
    public static PlayerController me;

    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        Move();

        if(Input.GetMouseButton(0) && Time.time - lastAttackTime > attackRate)
        {
            Attack();
        }

        float mouseX = (Screen.width / 2) - Input.mousePosition.x;

        if(mouseX < 0)
        {
            weaponAnim.transform.parent.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            weaponAnim.transform.parent.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Move()
    {
        // get the horizontal and vertical inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // apply that to our velocity
        rig.linearVelocity = new Vector2(x, y);

    }

    void Attack()
    {
        lastAttackTime = Time.time;

        // calculate the direction
        Vector3 dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;

        // shoot a raycast in the direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir, attackRange);

        // did we hit an enemy?
        if(hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            // get the enemy and damage them
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.photonView.RPC("TakeDamage", RpcTarget.MasterClient, damage);
        }
    }
}
