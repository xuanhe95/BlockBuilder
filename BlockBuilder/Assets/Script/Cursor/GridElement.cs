using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class coord
{
    public int x, y, z;

    public coord(int setX, int setY, int setZ)
    {
        x = setX;
        y = setY;
        z = setZ;
    }

}
public class GridElement : MonoBehaviour
{
    // Start is called before the first frame update

    private coord Coord;
    private Collider col;
    private Renderer rend;
    private bool isEnabled;
    private float elementHeight;
    public CornerElement[] corners = new CornerElement[8];

    public void Initialize(int x, int y, int z, float eleHeight)
    {
        int width = LevelGenerator.instance.width;
        int height = LevelGenerator.instance.height;
        
        Coord = new coord(x, y, z);
        this.name = "GE_" + x + y + z;
        this.elementHeight = eleHeight;
        this.transform.localScale = new Vector3(1.0f, elementHeight, 1.0f);
        this.col = this.GetComponent<Collider>();
        this.rend = this.GetComponent<Renderer>();

        corners[0] = LevelGenerator.instance.cornerElements[Coord.x + (width + 1) * (Coord.z + (width + 1) * Coord.y)];
        corners[1] = LevelGenerator.instance.cornerElements[Coord.x + 1 + (width + 1) * (Coord.z + (width + 1) * Coord.y)];
        corners[2] = LevelGenerator.instance.cornerElements[Coord.x + (width + 1) * (Coord.z + 1 + (width + 1) * Coord.y)];
        corners[3] = LevelGenerator.instance.cornerElements[Coord.x + 1 + (width + 1) * (Coord.z + 1 + (width + 1) * Coord.y)];
        corners[4] = LevelGenerator.instance.cornerElements[Coord.x + (width + 1) * (Coord.z + (width + 1) * (Coord.y+1))];
        corners[5] = LevelGenerator.instance.cornerElements[Coord.x + 1 + (width + 1) * (Coord.z + (width + 1) *  (Coord.y+1))];
        corners[6] = LevelGenerator.instance.cornerElements[Coord.x + (width + 1) * (Coord.z + 1 + (width + 1) *  (Coord.y+1))];
        corners[7] = LevelGenerator.instance.cornerElements[Coord.x + 1 + (width + 1) * (Coord.z + 1 + (width + 1) *  (Coord.y+1))];
        StartCoroutine("Ini");


    }

    IEnumerator Ini()
    {
        yield return null;
        InitializeCornerPos();
    }
    private void InitializeCornerPos()
    {
        print("set");
        corners[0].SetPosition(col.bounds.min.x, col.bounds.min.y, col.bounds.min.z);
        corners[1].SetPosition(col.bounds.max.x, col.bounds.min.y, col.bounds.min.z);
        corners[2].SetPosition(col.bounds.min.x, col.bounds.min.y, col.bounds.max.z);
        corners[3].SetPosition(col.bounds.max.x, col.bounds.min.y, col.bounds.max.z);
        corners[4].SetPosition(col.bounds.min.x, col.bounds.max.y, col.bounds.min.z);
        corners[5].SetPosition(col.bounds.max.x, col.bounds.max.y, col.bounds.min.z);
        corners[6].SetPosition(col.bounds.min.x, col.bounds.max.y, col.bounds.max.z);
        corners[7].SetPosition(col.bounds.max.x, col.bounds.max.y, col.bounds.max.z);
    }

    public coord GetCoord()
    {
        return Coord;
    }

    public void SetEnable()
    {
        this.col.enabled = true;
        //this.rend.enabled = true;
        this.isEnabled = true;
        foreach (CornerElement ce in corners)
        {
            ce.SetCornerElement();
        }
    }
    public void SetDisable()
    {
        this.col.enabled = false;
        //this.rend.enabled = false;
        this.isEnabled = false;
        foreach (CornerElement ce in corners)
        {
            ce.SetCornerElement();
        }
    }

    public bool getEnabled()
    {
        return isEnabled;
    }

    public float getHeight()
    {
        return elementHeight;
    }
}
