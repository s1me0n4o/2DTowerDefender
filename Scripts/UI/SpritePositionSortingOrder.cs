using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    [SerializeField] float possitionOffsetY;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Setting order or Y to be lower as higher the Y is
    private void LateUpdate()
    {
        //percisionMultiplier should be based on size of the map that you have min/max is -32767/+32767
        var percisionMultiplier = 5f;
       _spriteRenderer.sortingOrder = (int)(-(transform.position.y + possitionOffsetY) * percisionMultiplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
