using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;


namespace Assets.Scripts.Controllers
{
    public class GenerateLevelController
    {
        private GenerateLevelView _view;
        private int[,] _map;
        private Dictionary<int, Tile> _tiles;
        
        public GenerateLevelController(GenerateLevelView view)
        {
            _view = view;
            _map = new int[view.WightMap, view.HeightMap];
            _tiles = new Dictionary<int, Tile>()
            {
                {0, null},
                {1, _view.GroundWithoutGrassCenter},
                {2, _view.GroundWithGrassCenter},
                {3, _view.GroundWithGrassRightTop},
                {4, _view.GroundWithGrassLeftTop},
                {5, _view.GroundWithoutGrassRightDown},
                {6, _view.GroundWithoutGrassDown},
                {7, _view.GroundWithoutGrassLeftDown}
            };
        }

        public void GenerateLevel()
        {
            var seed = Time.time.ToString(CultureInfo.InvariantCulture);
            string map = String.Empty;
            Random rand = new Random(seed.GetHashCode());
            for (int col = 0; col < _map.GetLength(0); col++)
            {
                for (int row = 0; row < _map.GetLength(1); row++)
                {
                    var persent = rand.Next(0, 100);
                    _map[col, row] = persent <= _view.PersentFill ? 1 : 0;
                    map += _map[col, row] + " ";
                }
                map += "\n";
            }
            
            Debug.Log(map);
            
            ChangeValues();
            SetTile();
        }

        private void ChangeValues()
        {
            Debug.Log("Изменение тайлов");
            for (int col = 0; col < _map.GetLength(0); col++)
            {
                for (int row = 0; row < _map.GetLength(1); row++)
                {
                    if (col == 0 || row == 0 ||
                        col == _map.GetLength(0) -  1||
                        row == _map.GetLength(1) - 1)
                    {
                        _map[col, row] = 1;
                        continue;
                    }
                    if (_map[col, row] == 1)
                    {
                        if (_map[col + 1, row] == 0)
                        {
                            if (_map[col + 1, row + 1] == 0)
                            {
                                _map[col, row] = 3;
                            }
                            else if (_map[col + 1, row - 1] == 0)
                            {
                                _map[col, row] = 4;
                            }
                            else _map[col, row] = 2;
                        }
                        else if (_map[col - 1, row] == 0)
                        {
                            if (_map[col + 1, row + 1] == 0)
                            {
                                _map[col, row] = 5;
                            }
                            else if (_map[col + 1, row - 1] == 0)
                            {
                                _map[col, row] = 7;
                            }
                            else _map[col, row] = 6;
                        }
                        else _map[col, row] = 1;
                    }
                }
            }
        }

        private void SetTile()
        {
            Debug.Log("Рисовка");
            for (int col = 0; col < _map.GetLength(0); col++)
            {
                for (int row = 0; row < _map.GetLength(1); row++)
                {
                    if(_map[col, row] != 0)
                        _view.TileMap.SetTile(new Vector3Int(_view.WightMap / 2 + col, _view.HeightMap / 2 + row, 0), _tiles[_map[col, row]]);
                }
            }
        }
    }
}