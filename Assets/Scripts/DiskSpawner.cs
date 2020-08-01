using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject diskPrefab;


    //function will be stacking the disks downward in the y position
    public void InitializeDiskStack(int numberOfDisks)
    {
        //x and z scale
        float xScaleOffset = 0.0f;
        float zScaleOffset = 0.0f;
        float initialXScale = diskPrefab.transform.localScale.x;
        float initialZScale = diskPrefab.transform.localScale.z;
        //y position
        float yPositionOffset = 0.0f;
        float initialYPosition = transform.position.y - diskPrefab.transform.localScale.y;

        for (int i = 0; i < numberOfDisks; i++)
        {
            float newXScale = initialXScale + xScaleOffset;
            float newZScale = initialZScale + zScaleOffset;
            float newYPosition = initialYPosition + yPositionOffset;

            //create new disk and set the scale of the disk
            GameObject newDisk = Instantiate(
                diskPrefab,
                new Vector3(transform.position.x, newYPosition, transform.position.z),
                transform.rotation
            );
            newDisk.transform.localScale = new Vector3(
                newXScale,
                newDisk.transform.localScale.y,
                newZScale
            );
            newDisk.name = "Disk " + i;

            //set the offsets
            xScaleOffset = newXScale * 0.4f + xScaleOffset * 0.5f;
            zScaleOffset = newZScale * 0.4f + zScaleOffset * 0.5f;
            yPositionOffset = yPositionOffset - 2 * diskPrefab.transform.localScale.y;

            //set parent of new disk to this spawner object and disk to a random color
            newDisk.transform.parent = transform;
            newDisk.GetComponent<Renderer>().material.color = GetRandomColor();
            newDisk.GetComponent<DiskController>().size = i;
        }
    }

    public void DestroyDisks()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    private Color GetRandomColor()
    {
        return new Color(
            UnityEngine.Random.Range(0, 1f),
            UnityEngine.Random.Range(0, 1f),
            UnityEngine.Random.Range(0, 1f)
        );
    }
}