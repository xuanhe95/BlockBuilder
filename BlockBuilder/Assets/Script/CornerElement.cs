using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerElement : MonoBehaviour
{
    private coord coord;
    public GridElement[] nearGrids = new GridElement[8];
    public int bitMaskValue;
    private MeshFilter mesh;
    public void Initialize(int setX, int setY, int setZ)
    {
        coord = new coord(setX, setY, setZ);
        this.name = "CE_" + setX + setY + setZ;
        mesh = this.GetComponent<MeshFilter>(); 
    }

    public void SetPosition(float x, float y, float z)
    {
        this.transform.position = new Vector3(x, y, z);
    }

    public void SetCornerElement()
    {
        bitMaskValue = Bitmask.GetBitMask(nearGrids);
        mesh.mesh = CornerMeshes.instance.GetCornerMesh(bitMaskValue, coord.y);
    }

    public void SetNearGrids()
    {
        int width = LevelGenerator.instance.width;
        int height = LevelGenerator.instance.height;

        if(coord.x < width && coord.y < height && coord.z < width)
        {
            //UpperNorthEast
            nearGrids[0] = LevelGenerator.instance.gridElements[coord.x + width * (coord.z + width * coord.y)];
        }
        if(coord.x > 0 && coord.y < height & coord.z < width)
        {
            //UpperNorthWest
            nearGrids[1] = LevelGenerator.instance.gridElements[coord.x - 1 + width * (coord.z + width * coord.y)];
        }
        if(coord.x > 0 && coord.y < height & coord.z > 0)
        {
            //UpperSouthWest
            nearGrids[2] = LevelGenerator.instance.gridElements[coord.x - 1 + width * (coord.z - 1 + width * coord.y)];
        }
        if(coord.x < width && coord.y < height && coord.z > 0)
        {
            //UpperSouthEast
            nearGrids[3] = LevelGenerator.instance.gridElements[coord.x + width * (coord.z - 1 + width * coord.y)];
        }


        if(coord.x < width && coord.y > 0 && coord.z < width)
        {
            //LowerNorthEast
            nearGrids[4] = LevelGenerator.instance.gridElements[coord.x + width * (coord.z + width * (coord.y - 1))];
        }
        if(coord.x > 0 && coord.y > 0 & coord.z < width)
        {
            //LowerNorthWest
            nearGrids[5] = LevelGenerator.instance.gridElements[coord.x - 1 + width * (coord.z + width * (coord.y - 1))];
        }
        if(coord.x > 0 && coord.y > 0 & coord.z > 0)
        {
            //LowerSouthWest
            nearGrids[6] = LevelGenerator.instance.gridElements[coord.x - 1 + width * (coord.z - 1 + width * (coord.y - 1))];
        }
        if(coord.x < width && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthEast
            nearGrids[7] = LevelGenerator.instance.gridElements[coord.x + width * (coord.z - 1 + width * (coord.y - 1))];
        }
    }
}
