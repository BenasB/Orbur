using UnityEngine;
using UnityEngine.UI;

namespace Blocker
{
    public class ExecuteButton : MonoBehaviour
    {
        [SerializeField] Image childImage;
        [SerializeField] Sprite playSprite;
        [SerializeField] Sprite stopSprite;
        [SerializeField] Button.ButtonClickedEvent executeEvent;
        [SerializeField] Button.ButtonClickedEvent stopEvent;

        Button button;

        void Start()
        {
            button = GetComponent<Button>();
        }

        public bool Executing
        {
            set
            {
                if (value)
                {
                    childImage.sprite = stopSprite;
                    button.onClick = stopEvent;
                }
                else
                {
                    childImage.sprite = playSprite;
                    button.onClick = executeEvent;
                }
            }
        }
    }
}