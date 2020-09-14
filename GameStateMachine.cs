using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using D2D.Utils;

namespace D2D.Core
{
    /// <summary>
    /// Powerful and safe game state machine which extensible (use classes instead of fixed enums)
    /// and allows to check is next state possible, count happened states
    /// </summary>
    public class GameStateMachine : MonoBehaviour, ILazyCreating
    {
        /// <summary>
        /// Is there is no states yet?
        /// </summary>
        public bool IsEmpty => states.Count == 0;

        /// <summary>
        /// Last state of game state machine or null if empty yet
        /// </summary>
        public GameState Last => IsEmpty ? null : states.Last();
        
        /// <summary>
        /// List of states happened during game
        /// </summary>
        private List<GameState> states = new List<GameState>();
        
        /// <summary>
        /// For each game state we have a list of subscribers' actions 
        /// </summary>
        private Dictionary<Type, List<Subscriber>> subscribersMap = 
            new Dictionary<Type, List<Subscriber>>();

        /// <summary>
        /// Attach some action to some game state
        /// </summary>
        public void AdjustActionToState<T>(GameObject owner, Action action) where T : GameState
        {
            // Init dictionary if needed
            if (!subscribersMap.ContainsKey(typeof(T)))
                subscribersMap[typeof(T)] = new List<Subscriber>();
            
            // Add new subscriber
            subscribersMap[typeof(T)].Add(new Subscriber
            {
                owner = owner,
                action = action,
            });
        }

        /// <summary>
        /// Push new game state and notify others of it
        /// </summary>
        public void PushState<T>() where T : GameState, new()
        {
            var newState = new T();

            if (!IsStatePossible(newState))
            {
                Debug.LogError($"{newState.GetType().FullName} can't go after " +
                               $"{Last.GetType().FullName}");
                return;
            }

            states.Add(newState);
            
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

            if (Last.PossibleNextStates.Count == 0)
                return false;
            
            foreach (GameState p in Last.PossibleNextStates)
            {
                if (p.GetType() == newState.GetType())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Notify others of some game state happened
        /// </summary>
        private void Emit(Type t)
        {
            if (subscribersMap.ContainsKey(t))
            {
                // There is nobody subscribed to this state
                if (subscribersMap[t] == null)
                    return;
                
                // Use for safety try catch
                var subscribers = subscribersMap[t];
                for (int i = 0; i < subscribers.Count; i++)
                {
                    // Remove subscriber if it gameObject or action isn`t exists anymore 
                    if (subscribers[i].owner == null || subscribers[i].action == null)
                    {
                        subscribers.RemoveAt(i);
                        Debug.Log("Already destroyed subscriber was removed!");
                    }
                    // If all is ok, execute subscriber`s action 
                    else
                    {
                        subscribers[i].action?.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// How many were states of some type?
        /// </summary>
        public int Count<T>() where T : GameState
        {
            int matches = 0;
            foreach (GameState s in states)
            {
                if (s.GetType() == typeof(T))
                    matches++;
            }

            return matches;
        }
    }
}