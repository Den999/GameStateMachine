using D2D.Core;
using D2D.Utils;
using UnityEngine;

namespace D2D
{
    /// <summary>
    /// Just to avoid same code writing. So many scripts usually hashes GSM, so here is a
    /// hasher template for them.
    /// </summary>
    public class GameStateMachineHasher : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        protected GameStateMachine StateMachine
        {
            get
            {
                if (_gameStateMachine == null)
                    _gameStateMachine = gameObject.FindOrCreate<GameStateMachine>();

                return _gameStateMachine;
            }
        }
    }
}