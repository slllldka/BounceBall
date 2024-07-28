using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : MonoBehaviour {
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Sprite copySprite;
    private Vector3 copyPos;
    private float startTime;
    public float delay, period;

    // Start is called before the first frame update
    void Start() {
        startTime = Time.time;

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        rb.gravityScale = 0;
        bc.isTrigger = true;
        bc.size = new Vector2(0.44f, 0.44f);

        copySprite = sr.sprite;
        copyPos = transform.position;

        StartCoroutine(StartFallingRock());
        StartCoroutine(CreateNew());
    }

    // Update is called once per frame
    void Update() {
        CheckOutOfBounds();
    }

    private IEnumerator StartFallingRock() {
        yield return new WaitForSeconds(delay);
        for (int i = 1; i <= 10; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i / 10f);
            yield return new WaitForSeconds(0.05f);
        }
        rb.gravityScale = 1;
        bc.isTrigger = false;
    }
    private IEnumerator CreateNew() {
        yield return new WaitForSeconds(period);
        GameObject copy = new GameObject("FallingRock");
        copy.tag = "FallingRock";
        copy.AddComponent<SpriteRenderer>();
        copy.AddComponent<Rigidbody2D>();
        copy.AddComponent<BoxCollider2D>();
        copy.AddComponent<FallingRockScript>();
        copy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        copy.GetComponent<SpriteRenderer>().sprite = copySprite;
        copy.GetComponent<Rigidbody2D>().gravityScale = 0;
        copy.GetComponent<Rigidbody2D>().simulated = true;
        copy.GetComponent<BoxCollider2D>().isTrigger = false;
        copy.GetComponent<FallingRockScript>().delay = delay;
        copy.GetComponent<FallingRockScript>().period = period;
        copy.transform.position = copyPos;
    }

    private void CheckOutOfBounds() {
        if (transform.position.y < -6) {
            DestroyFallingRock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        DestroyFallingRock();
    }

    private void DestroyFallingRock() {
        if(Time.time - startTime > period) {
            Destroy(gameObject);
        } else {
            Destroy(sr);
            Destroy(rb);
            Destroy(bc);
            StartCoroutine(WaitForCreate());
        }
    }
    private IEnumerator WaitForCreate() {
        while(Time.time - startTime < period) {
            yield return null;
        }
        Destroy(gameObject);
    }
}