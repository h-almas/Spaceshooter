using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : Enemy
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float timeBetweenShots = .5f;
    private Camera cam;
    private float timeSinceLastShot;
    private Vector3 direction = Vector3.right;
    private bool goUp = false;
    private bool dashDown = false;
    private float timeBeforeDash = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Vector3 pos = cam.ViewportToWorldPoint(new Vector3(Random.Range(-.25f, 1.25f), 1.25f, 10) );
        transform.position = pos;
        StartCoroutine(MoveToCenterOfPath());
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp) return;
        if (dashDown)
        {
            transform.Translate(Vector3.up*(speed*3*Time.deltaTime));
            return;
        }
        
        
        #region Move

        Vector3 posInView = cam.WorldToViewportPoint(transform.position);

        if (posInView.x > 1)
        {
            direction = Vector3.right;
        }
        else if (posInView.x < 0)
        {
            direction = Vector3.left;
        }
            
        transform.Translate(direction*(speed*Time.deltaTime));

        #endregion

        #region Shoot

        if (timeSinceLastShot >= timeBetweenShots)
        {
            Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
            timeSinceLastShot = 0;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }

        #endregion
    }

    private IEnumerator MoveToCenterOfPath()
    {
        float centerY = cam.ViewportToWorldPoint(new Vector3(0, 0.75f, 0)).y;
        float originY = transform.position.y;
        float t = 0;
        float speed = 0.5f;
        
        while (Math.Abs(transform.position.y-centerY) > 0.1f)
        {
            float y = Mathf.Lerp(originY, centerY, t);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            t += speed * Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }

        yield return new WaitForSeconds(timeBeforeDash);

        goUp = true;
        
        StartCoroutine(Dash());

    }

    private IEnumerator Dash()
    {
        Vector3 moveBack = transform.position + Vector3.up*1.5f;
        float originY = transform.position.y;
        
        //move back
        while (transform.position.y < moveBack.y)
        {
            transform.Translate(Vector3.down * (speed*Time.deltaTime));
            yield return new WaitForNextFrameUnit();
        }

        yield return new WaitForSeconds(.25f);

        goUp = false;
        dashDown = true;
    }
}