using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    private int _score = Player.Score;
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

        //Vector3 positionInViewSpace = _mainCamera.WorldToViewportPoint(transform.position);

        //if (positionInViewSpace.x < 0.0f)
        {
           // transform.position =
                //_mainCamera.ViewportToWorldPoint(new Vector3(1, positionInViewSpace.y, positionInViewSpace.z));
        }
        //else if (positionInViewSpace.x > 1.0f)
        {
            //transform.position =
               // _mainCamera.ViewportToWorldPoint(new Vector3(0, positionInViewSpace.y, positionInViewSpace.z));
        }

        //transform.rotation = Quaternion.Slerp(_initialRotationBL, Quaternion.Euler(
            //-tilt.x * Input.GetAxis("Horizontal"),tilt.y * Input.GetAxis("Vertical"), 0), 1);

        if (Input.GetKeyDown("space"))
        {
            Instantiate(projectile, weaponLocation.position, transform.rotation);
        }

        scoreText.text = "Score: " + _score;
        starsText.text = "Stars: " + stars;
    }
}
