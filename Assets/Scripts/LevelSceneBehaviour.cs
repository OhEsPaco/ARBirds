using System.Collections;
using UnityEngine;

public class LevelSceneBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject level;

    [SerializeField]
    private ARTrackedObject canon;

    private float distanceToRestart = 0.14f;

    private GameObject instantiatedLevel;

    private bool canRestart = true;

    // Start is called before the first frame update
    private void Start()
    {
        instantiatedLevel = Instantiate(level);
        instantiatedLevel.transform.parent = this.transform;
        instantiatedLevel.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (canRestart && Vector3.Distance(this.transform.position, canon.transform.position) < distanceToRestart)
        {
            StartCoroutine(RestartLevel());
        }
    }

    private IEnumerator RestartLevel()
    {
        canRestart = false;

        Destroy(instantiatedLevel);
        instantiatedLevel = Instantiate(level);
        instantiatedLevel.transform.parent = this.transform;
        instantiatedLevel.transform.localPosition = new Vector3();
        instantiatedLevel.SetActive(true);

        yield return new WaitForSeconds(5);

        canRestart = true;
    }
}