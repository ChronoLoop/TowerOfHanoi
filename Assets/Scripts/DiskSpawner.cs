using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject diskPrefab;

    private void Awake()
    {
        SpawnDiskStack(3);
    }

    //function will be stacking the disks downward in the y position
    private void SpawnDiskStack(int numberOfDisks)
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

            //set the offsets
            xScaleOffset = newXScale * 0.50f;
            zScaleOffset = newZScale * 0.50f;
            yPositionOffset = yPositionOffset - 2 * diskPrefab.transform.localScale.y;

            //set parent of new disk to this spawner object and disk to a random color
            newDisk.transform.parent = transform;
            newDisk.GetComponent<Renderer>().material.color = GetRandomColor();
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