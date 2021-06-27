using System;
using D2D.Utilities;
using D2D.Utils;
using UnityEngine;

namespace D2D.Core
{
    /// <summary>
    /// Just to avoid same code writing. So many scripts usually hashes GSM, so here is a.
    /// hasher template for them.
    /// </summary>
    public class GameStateMachineUser : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        protected GameStateMachine StateMachine
        {
            get
            {
                if (_gameStateMachine == null)
                {
                    _gameStateMachine = this.FindLazy<GameStateMachine>();
                    BindCallbacks();
                }

                return _gameStateMachine;
            }
        }

        private void BindCallbacks()
        {
            _gameStateMachine.On<RunningState>(OnGameRun);
            _gameStateMachine.On<PauseState>(OnGamePause);
            _gameStateMachine.On<WinState>(OnGameWin);
            _gameStateMachine.On<LoseState>(OnGameLose);
            _gameStateMachine.On<PostgameState>(OnPostgame);
            _gameStateMachine.On<WinState, LoseState>(OnGameFinish);
        }

        protected virtual void Awake()
        {
            if (_gameStateMachine == null)
            {
                _gameStateMachine = this.FindLazy<GameStateMachine>();
                BindCallbacks();
            }
        }

        protected virtual void OnGameRun()
        {
            
        }
        
        protected virtual void OnGamePause()
        {
            
        }
        
        protected virtual void OnGameWin()
        {
            
        }
        
        protected virtual void OnGameLose()
        {
            
        }
        
        protected virtual void OnPostgame()
        {
            
        }

        protected virtual void OnGameFinish()
        {
            
        }
    }
}