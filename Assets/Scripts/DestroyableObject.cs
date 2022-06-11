public class DestroyableObject : Targetable
{
    private Square ParentTile;

    private void Start()
    {
        ParentTile = transform.parent.GetComponent<Square>();
        ParentTile.CanBuild = false;
    }

    private void OnDestroy()
    {
        ParentTile.CanBuild = true;
    }
}    
