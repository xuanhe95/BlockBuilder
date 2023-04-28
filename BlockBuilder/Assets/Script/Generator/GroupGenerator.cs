using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator
{

    private List<GameObject> Waters;
    private List<GameObject> Sands;
    private List<GameObject> Lands;
    private List<GameObject> Trees;
    private List<GameObject> Emptys;

    public void generateMeshs()
    {
        Emptys = picker.ReadListFrom(Empty);
        Waters = picker.ReadListFrom(Water);
        Sands = picker.ReadListFrom(Sand);
        Lands = picker.ReadListFrom(Land);
        Trees = picker.ReadListFrom(Tree);
    }
}
