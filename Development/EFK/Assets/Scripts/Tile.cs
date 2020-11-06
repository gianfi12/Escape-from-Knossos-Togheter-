﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile
{
    private TileBase _tileBase;
    private Vector3Int _coordinates;

    public Tile(TileBase tileBase, Vector3Int coordinates)
    {
        _tileBase = tileBase;
        _coordinates = coordinates;
    }

    public TileBase TileBase
    {
        get => _tileBase;
    }

    public Vector3Int Coordinates
    {
        get => _coordinates;
        set => _coordinates = value;
    }
}