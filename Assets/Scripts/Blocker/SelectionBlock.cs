using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Blocker
{
    public class SelectionBlock : MonoBehaviour
    {
        [SerializeField] Text nameText;

        public MethodInfo MethodInfo { get; private set; }
        MonoBehaviour commandObject;
        Sequencer sequencer;

        public void Initliaze(MonoBehaviour commandObj, MethodInfo info, Sequencer seq)
        {
            commandObject = commandObj;
            MethodInfo = info;
            sequencer = seq;
            nameText.text = info.Name;
        }

        /// <summary>
        /// Called by the UI when the selection block is pressed
        /// </summary>
        public void AddToSequence()
        {
            sequencer.AddToSequence(this);
        }

        public void Execute(object[] parameters)
        {
            MethodInfo.Invoke(commandObject, parameters);
        }
    }
}