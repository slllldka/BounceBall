using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerScript : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private CircleCollider2D cc;

    private bool leftDown = false, rightDown = false, canMoveLeft = true, canMoveRight = true;
    private bool canDash = false, canJump = false;
    private int starCount = 0;
    private int lastArrowKeyCode = 0;
    private float lastKeyTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        starCount = 0;

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();

        leftDown = false;
        rightDown = false;
        canMoveLeft = true;
        canMoveRight = true;
        canDash = false;
        canJump = false;
        lastArrowKeyCode = 0;
        lastKeyTime = 0;
        sr.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
        checkLeftRight();
        moveLeftRight();
        checkDie();
    }

    private void checkLeftRight() {
        float currentTime = Time.time;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            leftDown = true;
            rightDown = false;
            if(lastArrowKeyCode == (int)KeyCode.LeftArrow) {
                if ((currentTime - lastKeyTime) < 0.4f) {
                    lastKeyTime = currentTime;
                    DashOrJump(((int)KeyCode.LeftArrow));
                } else {
                    lastKeyTime = currentTime;
                }
            } else {
                lastArrowKeyCode = (int)KeyCode.LeftArrow;
                lastKeyTime = currentTime;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rightDown = true;
            leftDown = false;
            if (lastArrowKeyCode == (int)KeyCode.RightArrow) {
                if ((currentTime - lastKeyTime) < 0.4f) {
                    lastKeyTime = currentTime;
                    DashOrJump(((int)KeyCode.RightArrow));
                } else {
                    lastKeyTime = currentTime;
                }
            } else {
                lastArrowKeyCode = (int)KeyCode.RightArrow;
                lastKeyTime = currentTime;
            }
        }
        if (!Input.GetKey(KeyCode.LeftArrow)) {
            leftDown = false;
        }
        if (!Input.GetKey(KeyCode.RightArrow)) {
            rightDown = false;
        }
    }

    private void moveLeftRight() {
        if (leftDown && canMoveLeft) {
            if(rb.gravityScale == 0) {
                rb.gravityScale = 1;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else {
                rb.gravityScale = 1;
                rb.velocity = new Vector2(0, rb.velocity.y);
                transform.position += new Vector3(-1.8f * Time.deltaTime, 0, 0);
            }
        }
        else if (rightDown && canMoveRight) {
            if (rb.gravityScale == 0) {
                rb.gravityScale = 1;
                rb.velocity = new Vector2(0, rb.velocity.y);
            } else {
                rb.gravityScale = 1;
                rb.velocity = new Vector2(0, rb.velocity.y);
                transform.position += new Vector3(1.8f * Time.deltaTime, 0, 0);
            }
        }
    }
    private void DashOrJump(int keyCode) {
        if (canDash) {
            canDash = false;
            rb.velocity = new Vector3(0, 0, 0);
            sr.color = Color.yellow;
            if (keyCode == (int)KeyCode.LeftArrow) {
                rb.AddForce(new Vector2(-5.0f, 1.5f), ForceMode2D.Impulse);
                leftDown = false;
            }
            else if (keyCode == (int)KeyCode.RightArrow) {
                rb.AddForce(new Vector2(5.0f, 1.5f), ForceMode2D.Impulse);
                rightDown = false;
            }
        }
        else if (canJump) {
            canJump = false;
            rb.velocity = new Vector3(0, 0, 0);
            sr.color = Color.yellow;
            rb.AddForce(new Vector2(0, 4.8f), ForceMode2D.Impulse);
        }
    }

    private void checkDie() {
        if(transform.position.y < -6) {
            Destroy(gameObject);
            GameManager.gm.openStage(GameManager.gm.currentStage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        rb.gravityScale = 1;
        if (collision.gameObject.CompareTag("Ground")) {
            if (collideLeftside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockRightMove(1f/3));
            } else if (collideRightside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockLeftMove(1f/3));
            }
        } else if (collision.gameObject.CompareTag("GroundTop")) {
            if (collideTopside(collision)) {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector2(0, 3.8f), ForceMode2D.Impulse);
            } else if (collideLeftside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockRightMove(1f/3));
            } else if (collideRightside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockLeftMove(1f/3));
            }
        } else if (collision.gameObject.CompareTag("CrackableGround")) {
            if (collideTopside(collision)) {
                Destroy(collision.gameObject);
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector2(0, 3.8f), ForceMode2D.Impulse);
            } else if (collideLeftside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockRightMove(1f / 3));
            } else if (collideRightside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockLeftMove(1f / 3));
            }
        } else if (collision.gameObject.CompareTag("Jump")) {
            if (collideTopside(collision)) {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector2(0, 6f), ForceMode2D.Impulse);
            } else if (collideLeftside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockRightMove(1f / 3));
            } else if (collideRightside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockLeftMove(1f / 3));
            }
        } else if (collision.gameObject.CompareTag("Thorn")) {
            if (collideTopside(collision)) {
                Destroy(gameObject);
                GameManager.gm.openStage(GameManager.gm.currentStage);
            } else if (collideLeftside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockRightMove(1f / 3));
            } else if (collideRightside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockLeftMove(1f / 3));
            }
        } else if (collision.gameObject.CompareTag("LeftMover")) {
            if (collideTopside(collision)) {
                rb.gravityScale = 0;
                transform.position = (collision.transform.position + new Vector3(-0.25f, 0, 0));
                rb.velocity = new Vector3(-5f, 0, 0);
            } else if (collideLeftside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockRightMove(1f / 3));
            } else if (collideRightside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockLeftMove(1f / 3));
            }
        } else if (collision.gameObject.CompareTag("RightMover")) {
            if (collideTopside(collision)) {
                rb.gravityScale = 0;
                transform.position = (collision.transform.position + new Vector3(0.25f, 0, 0));
                rb.velocity = new Vector3(5f, 0, 0);
            } else if (collideLeftside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockRightMove(1f / 3));
            } else if (collideRightside(collision)) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                StartCoroutine(blockLeftMove(1f / 3));
            }
        } else if (collision.gameObject.CompareTag("FallingRock")) {
            Destroy(gameObject);
            GameManager.gm.openStage(GameManager.gm.currentStage);
        }
    }

    private bool collideTopside(Collision2D collision) {
        float slope = collision.collider.bounds.size.y / collision.collider.bounds.size.x;
        return ((rb.transform.position.y > -0.9 * slope * (rb.transform.position.x - collision.gameObject.transform.position.x) + collision.gameObject.transform.position.y)
            && (rb.transform.position.y > 0.9 * slope * (rb.transform.position.x - collision.gameObject.transform.position.x) + collision.gameObject.transform.position.y));
    }
    private bool collideLeftside(Collision2D collision) {
        float slope = collision.collider.bounds.size.y / collision.collider.bounds.size.x;
        return ((rb.transform.position.y <= -0.9 * slope * (rb.transform.position.x - collision.gameObject.transform.position.x) + collision.gameObject.transform.position.y)
            && (rb.transform.position.y >= 1.1 * slope * (rb.transform.position.x - collision.gameObject.transform.position.x) + collision.gameObject.transform.position.y));
    }
    private bool collideRightside(Collision2D collision) {
        float slope = collision.collider.bounds.size.y / collision.collider.bounds.size.x;
        return ((rb.transform.position.y >= -1.1 * slope * (rb.transform.position.x - collision.gameObject.transform.position.x) + collision.gameObject.transform.position.y)
            && (rb.transform.position.y <= 0.9 * slope * (rb.transform.position.x - collision.gameObject.transform.position.x) + collision.gameObject.transform.position.y));
    }
    private IEnumerator blockLeftMove(float seconds) {
        canMoveLeft = false;
        yield return new WaitForSeconds(seconds);
        canMoveLeft = true;
    }
    private IEnumerator blockRightMove(float seconds) {
        canMoveRight = false;
        yield return new WaitForSeconds(seconds);
        canMoveRight = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Star")) {
            starCount++;
            Destroy(collision.gameObject);
            if (starCount == MapReader.starNum) {
                if (PlayerPrefs.GetInt("ClearStage") < GameManager.gm.currentStage) {
                    PlayerPrefs.SetInt("ClearStage", GameManager.gm.currentStage);
                }
                GameManager.gm.openStage(GameManager.gm.currentStage + 1);
            }
        } else if (collision.gameObject.CompareTag("DashItem")) {
            Destroy(collision.gameObject);
            canDash = true;
            sr.color = Color.black;
        } else if (collision.gameObject.CompareTag("JumpItem")) {
            Destroy(collision.gameObject);
            canJump = true;
            sr.color = new Color(150f / 256, 75f / 256, 0);
        }
    }
}