﻿using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    private float inverseMoveTime;
    private bool facingRight = true;

    protected Animator animator;
    protected Rigidbody2D rBody;
    protected SpriteRenderer spRender;

    public float moveTime = 0.1f;
	public LayerMask blockingLayer = 1 << 8;

    protected virtual void Start() {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spRender = GetComponent<SpriteRenderer>();
    }

    protected void Walk(Vector3 finish) {
        if ((rBody.position.x - finish.x) < 0 && !facingRight) {
            Flip();
        }
        else if ((rBody.position.x - finish.x) > 0 && facingRight) {
            Flip();
        }
        RaycastHit2D hit = Physics2D.Linecast(transform.position, finish, blockingLayer);
        //Check if anything was hit
        if (hit.transform == null) {
            inverseMoveTime = 1f / moveTime;
			rBody.position = Vector3.MoveTowards(rBody.position, finish, inverseMoveTime * Time.deltaTime);
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}