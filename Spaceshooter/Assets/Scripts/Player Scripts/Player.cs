using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public static int Lives = 3;
    public static int Score = 0;
    private Quaternion _initialRotation;
    private Camera _mainCamera;
    [SerializeField] private float playerSpeed = 0.2f;
    [SerializeField] private float respawnTime = 1.5f;
    [SerializeField] private float invincibleTime = 1f;
    [SerializeField] public GameObject baseProjectile;
    private GameObject currentProjectile;

    public GameObject CurrentProjectile
    {
        get => currentProjectile;
        set
        {
            currentProjectile = value;
            currentWeapon.SetProjectile(value);
        }
    }
    [SerializeField] public Weapon baseWeapon;
    private Weapon currentWeapon;
    public Weapon CurrentWeapon
        {
            get => currentWeapon;
            set
            {
                currentWeapon.DoCleanUp();
                currentWeapon = value;
                currentWeapon.SetProjectile(currentProjectile);
                currentWeapon.SetTransform(weaponLocation);
            }
        }
    [SerializeField] private Vector2 tilt;
    [SerializeField] private Transform weaponLocation;
    public GameObject hitExplosion;
    private List<KeyCode> activateGodMode = new List<KeyCode>();

    private KeyCode[] godModeRequiredSequence =
    {
        KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A
    };

    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI livesText;
    public TMPro.TextMeshProUGUI scoreTextFinal;
    public TMPro.TextMeshProUGUI livesTextFinal;
    public TMPro.TextMeshProUGUI missedHitsFinal;
    
    public enum State
    {
        Playing,
        Explosion,
        Invincible,
        Godmode,
        Shielded
    }

    public State _playerState;

    void Start()
    {
        Score = 0;
        Lives = 3;
        _initialRotation = transform.rotation;
        _mainCamera = Camera.main;
        currentProjectile = baseProjectile;
        currentWeapon = baseWeapon;
        currentWeapon.SetProjectile(currentProjectile);
        currentWeapon.SetTransform(weaponLocation);
    }
    
    void Update()
    {
        if (_playerState != State.Godmode)
        {
            if (Input.GetKeyDown(godModeRequiredSequence[activateGodMode.Count]))
            {
                activateGodMode.Add(godModeRequiredSequence[activateGodMode.Count]);
               
                if (activateGodMode.Count == godModeRequiredSequence.Length)
                {
                    activateGodMode.Clear();
                    Debug.Log("Initiating GodMode");
                    _playerState = State.Godmode;
                    
                }
            }
            else if(Input.anyKeyDown)
            {
                activateGodMode.Clear();
            }
        }
        
        if (_playerState != State.Explosion)
        {
            float amtToMoveX = playerSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * amtToMoveX, Space.World);

            float amtToMoveY = playerSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * amtToMoveY, Space.World);

            Vector3 positionInViewSpace = _mainCamera.WorldToViewportPoint(transform.position);

            if (positionInViewSpace.x < 0.0f)
            {
                transform.position =
                    _mainCamera.ViewportToWorldPoint(new Vector3(1, positionInViewSpace.y, positionInViewSpace.z));
            }
            else if (positionInViewSpace.x > 1.0f)
            {
                transform.position =
                    _mainCamera.ViewportToWorldPoint(new Vector3(0, positionInViewSpace.y, positionInViewSpace.z));
            }
            
            if (positionInViewSpace.y < 0.0f)
            {
                transform.position =
                    _mainCamera.ViewportToWorldPoint(new Vector3(positionInViewSpace.x, 0, positionInViewSpace.z));
            }
            else if (positionInViewSpace.y > 1.0f)
            {
                transform.position =
                    _mainCamera.ViewportToWorldPoint(new Vector3(positionInViewSpace.x, 1, positionInViewSpace.z));
            }

            transform.rotation = Quaternion.Slerp(_initialRotation, Quaternion.Euler(0, -tilt.x * Input.GetAxis("Horizontal"), 0), 1);

            if (_playerState != State.Invincible)
            {
                currentWeapon.Shoot();
                
            }

            scoreText.text = "Score: " + Score;
            livesText.text = "Lives: " + Lives;

            scoreTextFinal.text = "Score " + Score;
            livesTextFinal.text = "Lives: " + Lives;
            missedHitsFinal.text = "Missed Hits: " + MissedEnemies.missedHits;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerState == State.Playing && (other.CompareTag("Enemy") || other.CompareTag("EnemyProjectile")))
        {
            Instantiate(hitExplosion, transform.position, transform.rotation);
            
            StartCoroutine(DestroyShip());
            
        }
    }

    private IEnumerator DestroyShip()
    {
        _playerState = State.Explosion;
        Instantiate(hitExplosion, transform.position, transform.rotation);
        var renderer = GetComponent<Renderer>();
        var collider = GetComponent<Collider>();
        renderer.enabled = false;
        collider.enabled = false;
        
        Lives--;
        yield return new WaitForSeconds(respawnTime);

        if (Lives <= 0)
        {
            SceneManager.LoadScene(8);
            Lives = 3;
        }
        else
        {
            renderer.enabled = true;
            transform.position = new Vector3(0, -2, 0);
            while (transform.position.y < 2)
            {
                float amtToMoVeY = Time.deltaTime * playerSpeed;
                transform.position += Vector3.up * amtToMoVeY;
                yield return null;
            }
            _playerState = State.Invincible;

            renderer.enabled = true;

            StartCoroutine(Blink());

            yield return new WaitForSeconds(invincibleTime);

            collider.enabled = true;
            
            _playerState = State.Playing;
            renderer.enabled = true;

        }
    }

    
    
    private IEnumerator Blink()
    {
        var renderer = GetComponent<Renderer>();
        while (_playerState == State.Invincible)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void IncLives()
    {
        Lives++;
    }
}