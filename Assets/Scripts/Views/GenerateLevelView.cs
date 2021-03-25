using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevelView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Vector2Int _sizeMap;
    [Header("Tiles")]
    [SerializeField] private Tile _groundWithoutGrassCenter;
    [SerializeField] private Tile _groundWithoutGrassLeftDown;
    [SerializeField] private Tile _groundWithoutGrassRightDown;
    [SerializeField] private Tile _groundWithoutGrassDown;
    [SerializeField] private Tile _groundWithGrassCenter;
    [SerializeField] private Tile _groundWithGrassRightTop;
    [SerializeField] private Tile _groundWithGrassLeftTop;
    [SerializeField, Range(0, 100)] private int _persentFill;
    
    public Tilemap TileMap => _tilemap;
    public Vector2Int SizeMap => _sizeMap;
    public int HeightMap => _sizeMap.y;
    public int WightMap => _sizeMap.x;

    public int PersentFill => _persentFill;

    public Tile GroundWithoutGrassCenter => _groundWithoutGrassCenter;
    public Tile GroundWithoutGrassLeftDown => _groundWithoutGrassLeftDown;
    public Tile GroundWithoutGrassRightDown => _groundWithoutGrassRightDown;
    public Tile GroundWithoutGrassDown => _groundWithoutGrassDown;
    public Tile GroundWithGrassCenter => _groundWithGrassCenter;
    public Tile GroundWithGrassRightTop => _groundWithGrassRightTop;
    public Tile GroundWithGrassLeftTop => _groundWithGrassLeftTop;
}
