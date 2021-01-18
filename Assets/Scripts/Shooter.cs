using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private float directionX = 1;

    [SerializeField]
    private float directionY = 1;

    [SerializeField]
    private float directionZ = 1;

    [SerializeField]
    private float arrowHeadLength = 0.25f;

    [SerializeField]
    private float arrowHeadAngle = 20.0f;

    [SerializeField]
    private float force = 10.0f;

    [SerializeField]
    private Transform aPoint;

    [SerializeField]
    private Transform bPoint;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform canon;

    [SerializeField]
    private float minimumDelayShot = 5;

    public GameObject scene;

    private GameObject newBullet;

    private bool isShooting = false;

    private Coroutine coroutineHandle;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        if(coroutineHandle !=null && isShooting)
        {
            StopCoroutine(coroutineHandle);
        }

        if (newBullet)
        {
            Destroy(newBullet);
        }

        isShooting = false;
    }

    public void Clap()
    {
        Debug.Log("CLAP");
        if (!isShooting)
        {
            if (newBullet)
            {
                Destroy(newBullet);
            }

            coroutineHandle = StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
       
        Vector3 direction = bPoint.position - aPoint.position;
        Vector3 forceVector = direction.normalized * force;

        newBullet = Instantiate(bullet, bPoint.position, bullet.transform.rotation);
        newBullet.transform.localScale = canon.transform.localScale;
        //newBullet.transform.rotation = canon.transform.rotation;
        newBullet.transform.parent = canon;
        newBullet.SetActive(true);

        newBullet.GetComponent<Rigidbody>().AddForceAtPosition(forceVector, bPoint.position, ForceMode.Impulse);
     
        yield return new WaitForSeconds(minimumDelayShot);
        isShooting = false;
    }

    private void OnDrawGizmos()
    {
        if (aPoint && bPoint)
        {
            Vector3 direction = bPoint.position - aPoint.position;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(bPoint.position, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(bPoint.position + direction, right * arrowHeadLength);
            Gizmos.DrawRay(bPoint.position + direction, left * arrowHeadLength);
        }
    }
}