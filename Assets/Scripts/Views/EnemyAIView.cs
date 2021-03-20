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
   [SerializeField] private ObjectView _protectorAIView;
   [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
   [SerializeField] private PatrolController _protectorAIPatrolPath;
   [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
   [SerializeField] private Transform[] _protectorWaypoints;
  
   #region Fields

   private PatrolAIController _simplePatrolAI;
   private StalkerAIController _stalkerAI;
   private ZombiePatrolController _protectorAI;
   private ProtectedZone _protectedZone;

   #endregion

  
   #region Unity methods

   private void Start()
   {
       _simplePatrolAI = new PatrolAIController(_simplePatrolAIView, new PatrolPathFindingModel(_simplePatrolAIConfig));
      
       _stalkerAI = new StalkerAIController(_stalkerAIView, new StalkerPathFindingModel(_stalkerAIConfig), _stalkerAISeeker, _stalkerAITarget);
       InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);
      
       _protectorAI = new ZombiePatrolController(_protectorAIView, new PatrolModel(_protectorWaypoints), _protectorAIDestinationSetter, _protectorAIPatrolPath);
       _protectorAI.Init();
      
       _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IPatrol>{ _protectorAI });
       _protectedZone.Init();
   }

   private void FixedUpdate()
   {
       if (_simplePatrolAI != null) _simplePatrolAI.FixedUpdate();
       if (_stalkerAI != null) _stalkerAI.FixedUpdate();
   }

   private void OnDestroy()
   {
       _protectorAI.Deinit();
       _protectedZone.Deinit();
   }

   #endregion

   #region Methods

   private void RecalculateAIPath()
   {
       _stalkerAI.RecalculatePath();
   }
      
   #endregion

    }
}