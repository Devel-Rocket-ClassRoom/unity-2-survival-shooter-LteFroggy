using UnityEngine;

static class Layers {
    public const string FloorName = "Floor";

    public static readonly int Floor = LayerMask.NameToLayer(FloorName);

    public static readonly int FloorMask = LayerMask.GetMask(FloorName);
}