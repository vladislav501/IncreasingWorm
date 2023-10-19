using UnityEngine;

public class WarmFood : MonoBehaviour
{
    [SerializeField] BoxCollider2D SpawnArea;

    private void Start()
    {
        RandomPosition();
    }


    private void RandomPosition()
    {
        Bounds bounds = this.SpawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 10f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "worm")
        {
            RandomPosition();
        }
    }
}


