using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Level")]

#nullable enable
public class Level : ScriptableObject
{
    public string Name => name;

    public bool IsLoaded { get; set; }

    public Level? Next;

    public Level? Previous;
}
