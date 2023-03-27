using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    public float yOffset;
    public float xOffset;

    private Transform childTransfrom;
    void Start()
    {
        this.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            Vector3 dir = parent.transform.position - this.transform.position;
            Vector3 moveVector = new Vector2((dir.x + xOffset), (dir.y + yOffset));
            this.transform.Translate(moveVector);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
