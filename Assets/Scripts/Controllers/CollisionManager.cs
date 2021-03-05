using System;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Assets.Scripts.Controllers
{
    public class CollisionManager : IDisposable
    {
        private Action<float> _action = delegate(float f) {  };
        private IView _character;
    private List<IView> _coins;
    private List<IView> _deathPoints;
    private IView _endPoint;
    private GameController _gameController;


    public CollisionManager(PlayerMove playerController, IView character, IView[] coins, IView[] deathPoints, IView endPoint, GameController controller)
    {
        _character = character;
        _coins = new List<IView>(coins);
        _deathPoints = new List<IView>(deathPoints);
        _endPoint = endPoint;
        _gameController = controller;
        character.OnCollision += CoinCollision;
        _action += playerController.Damage;
    }

    public void Refresh()
    {
        foreach (var item in _coins)
        {
            item.Transform.gameObject.SetActive(true);
        }
        foreach (var item in _deathPoints)
        {
            item.Transform.gameObject.SetActive(true);
        }
        _endPoint.Transform.gameObject.SetActive(true);
    }
    
    private void CoinCollision(IView obj)
    {
        obj?.Transform.gameObject.SetActive(false);
        if (_coins.Contains(obj))
        {
            Debug.Log("Подбор монетки!");
        }
        else if (_deathPoints.Contains(obj))
        {
            _gameController.Restart();
            Debug.Log("Вы проиграли!");
        }
        else if (obj == _endPoint)
        {
            _gameController.Restart();
            Debug.Log("Вы выиграли!");
        }
        else if(obj != null) _action.Invoke(1.0f);
    }

    public void Dispose()
    {
        _character = null;
        _coins.Clear();
        _deathPoints.Clear();
        _endPoint = null;
        _action = null;
    } 
    }
}