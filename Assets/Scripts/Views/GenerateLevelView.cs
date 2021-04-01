using UnityEngine;
using UnityEngine.Tilemaps;
using Views;


public class GenerateLevelView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Vector2Int _sizeMap;
    [SerializeField, Range(0, 100)] private int _persentFill;
    [SerializeField] private float _minDistancePlayerFromFinish;
    [Header("Tiles")]
    [SerializeField] private Tile _groundWithoutGrassCenter;
    [SerializeField] private Tile _groundWithoutGrassLeft;
    [SerializeField] private Tile _groundWithoutGrassLeftDown;
    [SerializeField] private Tile _groundWithoutGrassRight;
    [SerializeField] private Tile _groundWithoutGrassRightDown;
    [SerializeField] private Tile _groundWithoutGrassDown;
    [SerializeField] private Tile _groundWithGrassCenter;
    [SerializeField] private Tile _groundWithGrassRightTop;
    [SerializeField] private Tile _groundWithGrassLeftTop;

    [Header("SpawnPoints")] 
    [SerializeField] private Transform _playerStartTransform;
    [SerializeField] private Transform _finishTransform;

    [SerializeField] private Transform _deathZoneTransform;
    [SerializeField] private Transform _zombieTransform;
    [SerializeField] private QuestObjectView _money;
    [SerializeField] private int moneyCount;
    
    
    
    public Tilemap TileMap => _tilemap;
    public Vector2Int SizeMap => _sizeMap;
    public int HeightMap => _sizeMap.y;
    public int WightMap => _sizeMap.x;

    public int PersentFill => _persentFill;

    public float MinDistanceToFinish => _minDistancePlayerFromFinish;

    public Tile GroundWithoutGrassCenter => _groundWithoutGrassCenter;
    public Tile GroundWithoutGrassLeftDown => _groundWithoutGrassLeftDown;
    public Tile GroundWithoutGrassRightDown => _groundWithoutGrassRightDown;
    public Tile GroundWithoutGrassDown => _groundWithoutGrassDown;
    public Tile GroundWithGrassCenter => _groundWithGrassCenter;
    public Tile GroundWithGrassRightTop => _groundWithGrassRightTop;
    public Tile GroundWithGrassLeftTop => _groundWithGrassLeftTop;
    public Tile GroundWithoutGrassLeft => _groundWithoutGrassLeft;
    public Tile GroundWithoutGrassRight => _groundWithoutGrassRight;
    public Transform PlayerStartTransform => _playerStartTransform;
    public Transform DeathZoneTransform=> _deathZoneTransform;
    public Transform ZombieTransform => _zombieTransform;

    public Transform FinishTransform => _finishTransform;

    public QuestObjectView Money => _money;

    public int MoneyCount => moneyCount;
}
