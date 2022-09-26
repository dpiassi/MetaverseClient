
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.Game
{
    public abstract class AbstractSteps : MonoBehaviour
    {
        protected GameManager Manager;
        protected readonly Queue<Action> Steps = new();
        internal bool IsReady { get; private set; } = true;
        internal bool IsRunning { get; private set; } = false;

        protected abstract void InitSteps();

        protected void BlockThread()
        {
            IsReady = false;
        }

        protected void ResumeThread()
        {
            IsReady = true;
        }

        internal virtual void RunAll(GameManager gameManager)
        {
            Debug.Log("RUN ALL: " + Steps.Count);
            if (!Manager)
            {
                Manager = gameManager;
            }

            if (Manager)
            {
                IsRunning = true;
            }
        }


        private void Awake()
        {
            InitSteps();
        }

        private void Update()
        {
            if (IsRunning && IsReady)
            {
                if (Steps.TryDequeue(out Action action))
                {
                    IsReady = false;
                    action.Invoke();
                }
                else
                {
                    IsRunning = false;
                    Destroy(this);
                }
            }
        }
    }
}