using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    public static int score = 0;
    public static int lives = 3;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition1;
    [SerializeField] private Transform shootPosition2;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float shootDelaySec = 0.2f;
    [SerializeField] private int invincibilityTime = 1;
    [SerializeField] private ParticleSystem explosion;


    [SerializeField] private bool activateVerticalMovement = false;
    [SerializeField] private TMPro.TextMeshProUGUI scoreUI;
    [SerializeField] private TMPro.TextMeshProUGUI livesUI;

    private State playerState = State.Playing;
    private float timeSinceLastShot;


    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        score = 0;
    }

    void Update()
    {
        #region Screen Wrap/Clamp

        if (transform.position.x < -7.4f)
        {
            transform.position = new Vector3(7.4f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 7.4f)
        {
            transform.position = new Vector3(-7.4f, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
        }
        else if (transform.position.y > 4f)
        {
            transform.position = new Vector3(transform.position.x, 4f, transform.position.z);
        }

        #endregion

        if (playerState == State.Playing)
        {
            #region Shoot

            if (timeSinceLastShot >= shootDelaySec)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Instantiate(projectilePrefab, shootPosition1.position, Quaternion.identity);
                    Instantiate(projectilePrefab, shootPosition2.position, Quaternion.identity);
                }

                timeSinceLastShot = 0;
            }
            else
            {
                timeSinceLastShot += Time.deltaTime;
            }

            #endregion
        }

        #region Move

        float amtToMoveHorizontally = playerSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * amtToMoveHorizontally);

        if (activateVerticalMovement)
        {
            float amtToMoveVertically = playerSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
            transform.Translate(Vector3.up * amtToMoveVertically);
        }

        #endregion

        scoreUI.text = "Score: " + score;
        livesUI.text = "Lives: " + lives;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerState == State.Playing && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.OnHit();
            lives--;
            StartCoroutine(Respawn());
            
            Debug.Log("We've been hit by: " + other.name + " Lives left: " + lives);

            if (lives <= 0)
            {
                scoreUI.text = "Score: " + score;
                livesUI.text = "Lives: " + lives;
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    public IEnumerator Respawn()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        
        playerState = State.Invincible;
        StartCoroutine(Blinking());
        yield return new WaitForSeconds(invincibilityTime);
        playerState = State.Playing;
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 10f));
        float t = 0.1f;
        Vector3 initialPosition = transform.position;
        
        while (Vector3.Distance(transform.position, center) > 0.01)
        {
            transform.position = Vector3.Lerp(initialPosition, center, t*Time.deltaTime);
            t += 0.1f;
            yield return new WaitForSeconds(0.05f);
        
        }
        transform.position = center;
        playerState = State.Playing;

        yield return null;
    }

    private IEnumerator Blinking()
    {
        Renderer renderer = GetComponent<Renderer>();
        
        while (playerState == State.Invincible)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.25f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.25f);
        }
    }

    private enum State
    {
        Playing,
        Invincible
    }
}