using UnityEngine;

static class Layers {
    public const string FloorName = "Floor";
    public const string PlayerName = "Player";

    public static readonly int Floor = LayerMask.NameToLayer(FloorName);
    public static readonly int Player = LayerMask.NameToLayer(PlayerName);

    public static readonly int FloorMask = LayerMask.GetMask(FloorName);
    public static readonly int PlayerMask = LayerMask.GetMask(PlayerName);
}