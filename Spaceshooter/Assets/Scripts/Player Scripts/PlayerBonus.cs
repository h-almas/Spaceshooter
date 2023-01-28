using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    public static int stars = 0;
    private Quaternion _initialRotationBL;

    [SerializeField] private float playerSpeed;
    [SerializeField] private Vector2 tilt;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform weaponLocation;

    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI starsText;
    public TMPro.TextMeshProUGUI scoreTextFinal;
    public TMPro.TextMeshProUGUI starsTextFinal;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Player.Score = 0;
        _initialRotationBL = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMoveX = playerSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * amtToMoveX, Space.World);

        float amtToMoveY = playerSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * amtToMoveY, Space.World);

        transform.rotation = Quaternion.Slerp(_initialRotationBL, Quaternion.Euler
            (tilt.x * Input.GetAxis("Vertical"),0, -90), 1);

        if (Input.GetKeyDown("space"))
        {
            Instantiate(projectile, weaponLocation.position, transform.rotation);
        }

        scoreText.text = "Score: " + Player.Score;
        starsText.text = "Stars: " + stars;
        scoreTextFinal.text = "SCORE: " + Player.Score;
        starsTextFinal.text = "STARS: " + stars;
    }
}
