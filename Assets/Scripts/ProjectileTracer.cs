using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileTracer : MonoBehaviour
{

    [SerializeField]
    private float startVelocityX=1;

    [SerializeField]
    private float startVelocityY = 1;

    [SerializeField]
    private float startVelocityZ = 1;
    // Start is called before the first frame update
    private void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        DrawTraject(gameObject.transform.position, new Vector3(startVelocityX, startVelocityY, startVelocityZ));
    }

    private void DrawTraject(Vector3 startPos, Vector3 startVelocity)
    {
        int verts = 200;
        LineRenderer line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = verts;

        Vector3 pos = startPos;
        Vector3 vel = startVelocity;
        Vector3 grav = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z);
        for (var i = 0; i < verts; i++)
        {
            line.SetPosition(i, new Vector3(pos.x, pos.y, pos.z));
            vel = vel + grav * Time.fixedDeltaTime;
            pos = pos + vel * Time.fixedDeltaTime;
        }
    }
}