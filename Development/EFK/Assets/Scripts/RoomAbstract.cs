﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class RoomAbstract : MonoBehaviour
{
    [SerializeField] protected AssetsCollection assetsCollection;

    public readonly List<Tile> Entrance = new List<Tile>();
    public readonly List<Tile> Exit = new List<Tile>();
    public readonly List<Tile> Wall = new List<Tile>();
    public readonly List<Tile> Floor = new List<Tile>();
    public readonly List<Transform> Object = new List<Transform>();
    public readonly List<Tile> Spawn = new List<Tile>();
    public readonly List<Tile> TileList = new List<Tile>();
    
    protected int _requiredWidthSpace;
    protected int _displacementX, _displacementY;

    public int RequiredWidthSpace => _requiredWidthSpace;

    public int DisplacementX => _displacementX;

    public int DisplacementY => _displacementY;

    public AssetsCollection AssetsCollection => assetsCollection;

    public abstract void Generate();

    public abstract void PlaceRoom(Tilemap tilemapFloor, Tilemap tilemapWall, Tilemap tilemapObject,
        Vector3Int coordinates);
}