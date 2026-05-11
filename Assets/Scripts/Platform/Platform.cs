using UnityEngine;

public class Platform : MonoBehaviour
{
    [Space(5)]
    public float moveDistance = 10.0f; 

    private float moveSpeed = 5.0f;

    private bool shouldGoDown = false;

    private Vector3 startPos;
    private Vector3 endPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.down * moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowards();
    }

    void MoveTowards()
    {
        if (shouldGoDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
        }
        
    }
    public void GoDown()
    {
        shouldGoDown = true;
    }
}
