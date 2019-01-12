using UnityEngine;
using System.Reflection;

namespace Blocker
{
    public class BlockManager : MonoBehaviour
    {
        /// <summary>
        /// An instance of the script whose command methods are presented on the UI 
        /// </summary>
        [SerializeField] MonoBehaviour commandObject;
        [SerializeField] GameObject selectionBlockPrefab;
        [SerializeField] Sequencer sequencer;

        void Start()
        {
            CreateSelectionBlocks();
        }

        /// <summary>
        /// Creates the blocks which only contain the function name on the selection panel
        /// </summary>
        void CreateSelectionBlocks()
        {
            MethodInfo[] mInfos = commandObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (MethodInfo m in mInfos)
            {
                Command[] commands = m.GetCustomAttributes(typeof(Command), true) as Command[];

                foreach (Command com in commands)
                {              
                    GameObject newGameObject = Instantiate(selectionBlockPrefab, transform);
                    SelectionBlock blockScript = newGameObject.GetComponent<SelectionBlock>();
                    if (blockScript != null)
                        blockScript.Initliaze(commandObject, m, sequencer);
                }
            }
        }
    }
}