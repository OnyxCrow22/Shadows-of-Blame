using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject lift;
    public GameObject player;
    public RaycastMaster rMaster;
    public Animator[] liftDoors;
    public bool atTop = false;
    public bool atBottom = false;
    public float liftSpeed = 3f;
    Vector3 topPos;
    Vector3 targetPos;
    Vector3 bottomPos;

    private void Start()
    {
        atBottom = true;
        atTop = false;
        bottomPos = lift.transform.position;
        topPos = bottomPos + new Vector3(0, 48.5f, 0);
        targetPos = bottomPos;
    }

    public void Update()
    {
        if (lift.transform.position != targetPos)
        {
            lift.transform.position = Vector3.MoveTowards(lift.transform.position, targetPos, liftSpeed * Time.deltaTime);
        }
    }

    public IEnumerator OperateLift()
    {
        liftDoors[0].SetBool("closingDoors", true);
        liftDoors[1].SetBool("closingDoors", true);
        liftDoors[0].SetBool("openingDoors", false);
        liftDoors[1].SetBool("openingDoors", false);
        yield return new WaitForSeconds(2);
        if (rMaster.inLift)
        {
            player.transform.parent = lift.transform;
        }    
        targetPos = topPos;
        yield return new WaitForSeconds(16.1f);
        atBottom = false;
        atTop = true;
        yield return new WaitForSeconds(2);
        liftDoors[0].SetBool("openingDoors", true);
        liftDoors[2].SetBool("openingDoors", true);
        liftDoors[0].SetBool("closingDoors", false);
        liftDoors[2].SetBool("closingDoors", false);
        rMaster.buttonPressed = false;
        player.transform.parent = null;
    }

    public IEnumerator GoingDown()
    {
        liftDoors[0].SetBool("openingDoors", false);
        liftDoors[2].SetBool("openingDoors", false);
        liftDoors[0].SetBool("closingDoors", true);
        liftDoors[2].SetBool("closingDoors", true);
        yield return new WaitForSeconds(2);
        if (rMaster.inLift)
        {
            player.transform.parent = lift.transform;
        }
        targetPos = bottomPos;
        yield return new WaitForSeconds(16.1f);
        atBottom = true;
        atTop = false;
        yield return new WaitForSeconds(1);
        liftDoors[0].SetBool("closingDoors", false);
        liftDoors[1].SetBool("closingDoors", false);
        liftDoors[0].SetBool("openingDoors", true);
        liftDoors[1].SetBool("openingDoors", true);
        rMaster.buttonPressed = false;
        player.transform.parent = null;
    }
}
