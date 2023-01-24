using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    public static int stars = 0;
    private Quaternion _initialRotationBL;
    private Camera _mainCamera;

    [SerializeField] private float playerSpeed;
    [SerializeField] private Vector2 tilt;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform weaponLocation;

    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI starsText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _initialRotationBL = transform.rotation;
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMoveX = playerSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * amtToMoveX, Space.World);

        float amtToMoveY = playerSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * amtToMoveY, Space.World);

        transform.rotation = Quaternion.Slerp(_initialRotationBL, Quaternion.Euler
            (0,90, -90 + tilt.y * Input.GetAxis("Vertical")), 1);

        if (Input.GetKeyDown("space"))
        {
            Instantiate(projectile, weaponLocation.position, transform.rotation);
        }

        scoreText.text = "Score: " + Player.Score;
        starsText.text = "Stars: " + stars;
    }
}
