using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Datas;
using Models;
using Pathfinding;
using UnityEngine;


namespace Views
{
    public class EnemyAIView : MonoBehaviour
    { 
        
   [Header("Simple AI")]
   [SerializeField] private AIConfig _simplePatrolAIConfig;
   [SerializeField] private ObjectView _simplePatrolAIView;

   [Header("Stalker AI")]
   [SerializeField] private AIConfig _stalkerAIConfig;
   [SerializeField] private ObjectView _stalkerAIView;
   [SerializeField] private Seeker _stalkerAISeeker;
   [SerializeField] private Transform _stalkerAITarget;

   [Header("Protector AI")] 
   [SerializeField] private AIConfig _protectorConfig;
   [SerializeField] private ObjectView _protectorAIView;
   //[SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
   //[SerializeField] private PatrolAIPath _protectorAIPatrolPath;
   [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
   [SerializeField] private Seeker _protecterSeeker;

   #region Fields

   private PatrolAIController _simplePatrolAI;
   private StalkerAIController _stalkerAI;
   private ProtecterController _protectorAI;
   private ProtectedZone _protectedZone;

   #endregion

  
   #region Unity methods

   private void Start()
   {
       if(_simplePatrolAIView != null)
        _simplePatrolAI = new PatrolAIController(_simplePatrolAIView, new PatrolPathFindingModel(_simplePatrolAIConfig));
      
       if(_stalkerAIView != null)
        _stalkerAI = new StalkerAIController(_stalkerAIView, new StalkerPathFindingModel(_stalkerAIConfig), _stalkerAISeeker, _stalkerAITarget);
       Debug.Log(_stalkerAI == null);
       
       if (_protectorAIView != null)
       {
           _protectorAI = new ProtecterController(_protectorAIView, new ProtecterModel(_protectorConfig), _protecterSeeker);
           //_protectorAI.Init();
       }

       if (_protectedZoneTrigger != null)
       {
           _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IProtected> {_protectorAI});
           _protectedZone.Init();
       }
       InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);
   }

   private void FixedUpdate()
   {
       _simplePatrolAI?.FixedUpdate();
       _stalkerAI?.FixedUpdate();
       _protectorAI?.FixedUpdate();
   }

   private void OnDestroy()
   {
       //_protectorAI?.Deinit();
       _protectedZone?.Deinit();
   }

   #endregion

   #region Methods

   private void RecalculateAIPath()
   {
       _stalkerAI?.RecalculatePath();
       _protectorAI?.RecalculatePath();
   }
      
   #endregion

    }
}