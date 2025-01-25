using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject BackWall;
    public GameObject FrontWall;
    public GameObject VisitedIndicator;
    public bool IsVisited;

    public int x => (int)transform.position.x;
    public int z => (int)transform.position.z;
    public void Visit()
    {
        VisitedIndicator.SetActive(false);
        IsVisited = true;
    }
}
