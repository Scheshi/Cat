using System;
using Pathfinding;

namespace Assets.Scripts.Controllers
{
    public class PatrolAIPath : AIPath
    {
        #region Events

        public event EventHandler TargetReached;

        #endregion


        #region Inheritance

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            DispatchTargetReached();
        }

        #endregion


        #region Methods

        protected virtual void DispatchTargetReached()
        {
            TargetReached?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
    
}