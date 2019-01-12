using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Blocker
{
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] GameObject blockPrefab;
        [SerializeField] float executionDelay = 1;

        List<Block> blocks = new List<Block>();
        Coroutine executionCoroutine;

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
            if (executionCoroutine != null)
                StopCoroutine(executionCoroutine);

            executionCoroutine = StartCoroutine(ExecutionCoroutine());
        }

        IEnumerator ExecutionCoroutine()
        {
            foreach (Block block in blocks)
            {
                block.SelectedBlock.Execute(block.Parameters);
                yield return new WaitForSeconds(executionDelay);
            }
        }
    }
}

