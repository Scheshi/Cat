using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = System.Object;
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
                {7, _view.GroundWithoutGrassLeftDown},
                {8, _view.GroundWithoutGrassRight},
                {9, _view.GroundWithoutGrassLeft}
            };
        }

        public void GenerateLevel()
        {
            var seed = Time.time.ToString(CultureInfo.InvariantCulture);
            string map = String.Empty;
            Random rand = new Random(seed.GetHashCode());
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    var persent = rand.Next(0, 100);
                    _map[x, y] = persent <= _view.PersentFill ? 1 : 0;
                    map += _map[x, y] + " ";
                }
                map += "\n";
            }
            
            //Debug.Log(map);
            
            ChangeValues();
            //ChangeValues(); При втором вызове вылетает IndexOutOfRangeException. Почему - ума не приложу.
            //Буду благодарен, если объясните.
            SetTile();
            SetTransforms();
        }

        private void ChangeValues()
        {
            Debug.Log("Изменение тайлов");
            string map = String.Empty;
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    try
                    {
                        if (x == 0 || y == 0 ||
                            x == _map.GetLength(0) - 1 ||
                            y == _map.GetLength(1) - 1)
                        {
                            _map[x, y] = 1;
                        }

                        if (_map[x, y] == 1)
                        {
                            if (_map[x, y + 1] != 0)
                            {
                                if (y > 1 && _map[x, y - 1] == 0)
                                {
                                    _map[x, y] = 5;
                                }
                                else _map[x, y] = 8;
                            }

                            else if (y > 1 && _map[x, y - 1] != 0)
                            {
                                if (_map[x, y + 1] == 0)
                                {
                                    _map[x, y] = 7;
                                }
                                else _map[x, y] = 8;
                            }

                            if (_map[x + 1, y] != 0)
                            {
                                if (x > 1 && _map[x - 1, y] == 0)
                                {
                                    _map[x, y] = 4;
                                }
                                else _map[x, y] = 2;
                            }
                            else if (x > 1 && _map[x - 1, y] != 0)
                            {
                                if (_map[x + 1, y] == 0)
                                {
                                    _map[x, y] = 3;
                                }
                                else _map[x, y] = 2;
                            }

                            if (_map.GetLength(0) > x &&
                                _map.GetLength(1) > y &&
                                x > 1 && y > 1 &&
                                _map[x - 1, y] != 0 &&
                                _map[x + 1, y] != 0 &&
                                _map[x, y + 1] != 0 &&
                                _map[x, y - 1] != 0)
                            {
                                _map[x, y] = 1;
                            }
                        }
                        else map += "0 ";

                        map += _map[x, y] + " ";
                    }
                    catch
                    {
                        //Debug.LogErrorFormat("Вышел за пределы массива на " + x + ", " + y);
                        continue;
                    }
                }
                map += "\n";
            }
            //Debug.Log(map);
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

        private void SetTransforms()
        {
            SetStartPointPlayer();
            SetFinishPoint();
            SetStalkerPosition();
            SetDeathZonePosition();
            SetMoneyObjects();
        }

        private void SetStartPointPlayer()
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    try
                    {
                        if (_map[x - 1, y] == 0 && _map[x - 2, y] == 0)
                        {
                            _view.PlayerStartTransform.position =
                                new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0);
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void SetFinishPoint()
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    try
                    {
                        if (_map[x + 1, y] == 0 &&
                            _map[x - 1, y] == 0 &&
                            _map[x, y] == 0 &&
                            _map[x, y + 1] == 0 &&
                            _map[x, y - 1] != 0 &&
                            (new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0) - _view.PlayerStartTransform.position).sqrMagnitude >=
                            _view.MinDistanceToFinish * _view.MinDistanceToFinish )
                        {
                            _view.FinishTransform.position =
                                new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0);
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void SetStalkerPosition()
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    try
                    {
                        if (_map[x - 1, y] == 0 && _map[x - 2, y] == 0 &&
                            (new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0) -
                             _view.PlayerStartTransform.position).sqrMagnitude >=
                            _view.MinDistanceToFinish * _view.MinDistanceToFinish)
                        {
                            _view.ZombieTransform.position =
                                new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0);
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void SetDeathZonePosition()
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    try
                    {
                        if (_map[x + 1, y] == 0 &&
                            _map[x - 1, y] == 0 &&
                            _map[x, y] == 0 &&
                            _map[x, y + 1] == 0 &&
                            _map[x, y - 1] != 0 &&
                            (new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0) - _view.PlayerStartTransform.position).sqrMagnitude >=
                            _view.MinDistanceToFinish * _view.MinDistanceToFinish )
                        {
                            _view.DeathZoneTransform.position =
                                new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0);
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void SetMoneyObjects(int count = 0)
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    try
                    {
                        if (_map[x + 1, y] == 0 &&
                            _map[x - 1, y] == 0 &&
                            _map[x, y] == 0 &&
                            _map[x, y + 1] == 0 &&
                            _map[x, y - 1] != 0 &&
                            UnityEngine.Random.value > 0.9)
                        {
                            var money = UnityEngine.Object.Instantiate(_view.Money);
                            money.transform.position =
                                new Vector3Int(_view.WightMap / 2 + x, _view.HeightMap / 2 + y, 0);
                            count++;
                            if (count > _view.MoneyCount) return;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            if(count < _view.MoneyCount) SetMoneyObjects(count);
        }
    }
}