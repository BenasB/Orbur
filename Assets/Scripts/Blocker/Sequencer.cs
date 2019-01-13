using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Blocker
{
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] GameObject blockPrefab;
        [SerializeField] float executionDelay = 1;
        [SerializeField] List<GameObject> objectsToReset = new List<GameObject>();

        [Header("UI")]
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] Button clearButton;
        [SerializeField] ExecuteButton executeButton;

        List<Block> blocks = new List<Block>();
        Coroutine executionCoroutine;


        private bool executingSequence;

        public bool ExecutingSequence
        {
            get { return executingSequence; }
            set
            {
                executeButton.Executing = value;
                clearButton.interactable = !value;
                executingSequence = value;
            }
        }


        public void AddToSequence(SelectionBlock selectionBlock)
        {
            GameObject newBlock = Instantiate(blockPrefab, transform);
            Block blockScript = newBlock.GetComponent<Block>();

            if (blockScript == null)
            {
                Debug.LogError("Could not find Block script on the Block Prefab");
                return;
            }

            blockScript.Initialize(selectionBlock.MethodInfo, selectionBlock);
            blocks.Add(blockScript);

            // Scroll the sequence to the bottom
            scrollRect.normalizedPosition = Vector2.down;
        }

        public void Execute()
        {
            Stop();

            executionCoroutine = StartCoroutine(ExecutionCoroutine());
        }

        public void Stop()
        {
            if (executionCoroutine != null)
            {
                StopCoroutine(executionCoroutine);
                ExecutingSequence = false;
            }

            ResetAllObjects();
        }

        public void RemoveAllBlocks()
        {
            if (ExecutingSequence)
                return;

            foreach (Block block in blocks)
            {
                Destroy(block.gameObject);
            }

            blocks = new List<Block>();
        }

        void ResetAllObjects()
        {
            foreach (GameObject resettableObject in objectsToReset)
            {
                IResettable resettable = resettableObject.GetComponent<IResettable>();

                if (resettable == null)
                {
                    Debug.LogError(string.Format("Unable to find IResettable on {0}", resettableObject.name));
                    continue;
                }

                resettable.Reset();
            }
        }

        IEnumerator ExecutionCoroutine()
        {
            ExecutingSequence = true;
            foreach (Block block in blocks)
            {
                block.SelectedBlock.Execute(block.Parameters);
                yield return new WaitForSeconds(executionDelay);
            }
            ExecutingSequence = false;
        }
    }
}

