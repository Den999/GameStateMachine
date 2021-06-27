using System;
using System.Collections.Generic;
using System.Linq;
using D2D.Utilities;
using UnityEngine;
using D2D.Utils;

namespace D2D.Core
{
    /// <summary>
    /// Powerful and safe game state machine which extensible (use classes instead of fixed enums)
    /// and allows to check is next state possible, count happened states
    ///
    /// In fact we could do game states as SO. But, it won`t be so useful in code.
    /// </summary>
    public class GameStateMachine : MonoBehaviour, ILazy
    {
        /// <summary>
        /// Is there is no states yet?
        /// </summary>
        public bool IsEmpty => _states.Count == 0;

        /// <summary>
        /// Last state of game state machine or null if empty yet
        /// </summary>
        public GameState Last => IsEmpty ? null : _states.Last();
        
        /// <summary>
        /// List of states happened during game
        /// </summary>
        private readonly List<GameState> _states = new List<GameState>();
        
        /// <summary>
        /// For each game state we have a list of subscribers' actions 
        /// </summary>
        private readonly Dictionary<Type, List<Subscriber>> _subscribersMap = 
            new Dictionary<Type, List<Subscriber>>();

        public List<string> PushedStatesNames
        {
            get
            {
                if (_states.Count == 0)
                    return new List<string>();
                
                List<string> lines = new List<string>();
                _states.ForEach(state => lines.Add(state + ""));
                return lines;
            }
        }

        private void Start()
        {
            Push(new RunningState());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Push(new WinState());
        }

        /// <summary>
        /// Attach some action to some game state
        /// </summary>
        public void On<TState>(Action action, GameObject owner = null) where TState : GameState
        {
            // Init dictionary if needed
            if (!_subscribersMap.ContainsKey(typeof(TState)))
                _subscribersMap[typeof(TState)] = new List<Subscriber>();
            
            // Add new subscriber
            _subscribersMap[typeof(TState)].Add(new Subscriber
            {
                owner = owner,
                action = action,
            });
        }
        
        /// <summary>
        /// On with 2 game state binding.
        /// </summary>
        public void On<TState1, TState2>(Action action, GameObject owner = null) 
            where TState1 : GameState
            where TState2 : GameState
        {
            On<TState1>(action, owner);
            On<TState2>(action, owner);
        }
        
        /// <summary>
        /// On with 3 game state binding.
        /// </summary>
        public void On<TState1, TState2, TState3>(Action action, GameObject owner = null) 
            where TState1 : GameState
            where TState2 : GameState
            where TState3 : GameState
        {
            On<TState1>(action, owner);
            On<TState2>(action, owner);
            On<TState3>(action, owner);
        }

        /// <summary>
        /// Push new game state and notify others of it.
        /// </summary>
        public void Push(GameState newState)
        {
            if (!IsStatePossible(newState))
            {
                Debug.LogError($"{newState} can't go after {Last}");
                return;
            }

            if (_states.IsNullOrEmpty() && !newState.CanBeFirstState)
            {
                throw new Exception($"{newState} can't be the first!");
            }

            _states.Add(newState);
            
            Emit(newState.GetType());
            Emit(typeof(GameState));
        }

        /// <summary>
        /// Push a new state but with now exception if it is not possible to push.
        /// </summary>
        /// <param name="newState"></param>
        public void PushSafely(GameState newState)
        {
            if (!IsStatePossible(newState))
                return;
            
            if (_states.IsNullOrEmpty() && !newState.CanBeFirstState)
            {
                throw new Exception($"{newState} can't be the first!");
            }

            _states.Add(newState);
            
            Emit(newState.GetType());
            Emit(typeof(GameState));
        }

        /// <summary>
        /// Can new state go after last state?
        /// </summary>
        private bool IsStatePossible(GameState newState)
        {
            if (Last == null)
                return true;
            
            return Last.IsNextStatePossible(newState);
        }

        /// <summary>
        /// Notify others of some game state happened
        /// </summary>
        private void Emit(Type t)
        {
            if (_subscribersMap.ContainsKey(t))
            {
                // There is nobody subscribed to this state
                if (_subscribersMap[t] == null)
                    return;
                
                // Use for safety try catch
                var subscribers = _subscribersMap[t];
                for (int i = 0; i < subscribers.Count; i++)
                {
                    // Remove subscriber if it gameObject or action isn`t exists anymore 
                    if (subscribers[i].action == null)
                    {
                        // Remove died member
                        subscribers.RemoveAt(i);
                    }
                    // If all is ok, execute subscriber`s action 
                    else
                    {
                        try
                        {
                            subscribers[i].action?.Invoke();
                        }
                        catch (Exception e)
                        {
                            // Died member action... skip.
                        }
                    }
                }
            }
        }

        /// <summary>
        /// How many there were states of this type?
        /// </summary>
        public int Count<TState>() where TState : GameState
        {
            int matches = 0;
            foreach (GameState s in _states)
            {
                if (s.GetType() == typeof(TState))
                    matches++;
            }

            return matches;
        }
        
        /// <summary>
        /// Was this state emitted?
        /// </summary>
        public bool Was<TState>() 
            where TState : GameState
        {
            foreach (GameState s in _states)
            {
                if (s.GetType() == typeof(TState))
                    return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Was any of these states emitted?
        /// </summary>
        public bool WasAny<TState1, TState2>()
            where TState1 : GameState
            where TState2 : GameState
        {
            foreach (GameState s in _states)
            {
                if (s.GetType() == typeof(TState1) || s.GetType() == typeof(TState2))
                    return true;
            }
            
            return false;
        }
    }
}