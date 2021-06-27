using System;
using UnityEngine;

namespace D2D.Core
{
    /// <summary>
    /// Owner and its action. Used to unsub zero actions with already destroyed owners
    /// </summary>
    public class Subscriber
    {
        public GameObject owner;
        public Action action;
    }
}