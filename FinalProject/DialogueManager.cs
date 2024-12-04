using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonogameProject3_Spaceship
{
    public class DialogueManager
    {
        private List<string> dialogues = new List<string>() {
            "Woah, you've selected \"Hard\" mode",
            "This is basically the same as the others but more",
            "The green sword will be undodgable, but you can pass it as long as you're moving"
        };
        private int currentDialogueIndex;
        private float textSpeed;
        private float currentTextTime;
        private int currentCharacter;
        private bool isDialogueActive;
        private bool isTextComplete;
        private float lineWaitTime;
        private float currentWaitTime;

        public DialogueManager(List<string> dialogues, float textSpeed, float lineWaitTime)
        {
            this.dialogues = dialogues;
            this.textSpeed = textSpeed;
            this.lineWaitTime = lineWaitTime;
            this.currentDialogueIndex = 0;
            this.currentCharacter = 0;
            this.isDialogueActive = false;
            this.isTextComplete = false;
            this.currentTextTime = 0f;
            this.currentWaitTime = 0f;
        }

        public void StartDialogue()
        {
            isDialogueActive = true;
            currentDialogueIndex = 0;
            currentCharacter = 0;
            isTextComplete = false;
            currentWaitTime = 0f;
        }

        public void UpdateDialogue(GameTime gameTime)
        {
            if (!isDialogueActive) return;

            currentTextTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTextTime >= textSpeed)
            {
                currentCharacter++;
                currentTextTime = 0f;

                if (currentCharacter >= dialogues[currentDialogueIndex].Length)
                {
                    isTextComplete = true;
                }
            }

            if (isTextComplete)
            {
                currentWaitTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (currentWaitTime >= lineWaitTime)
                {
                    currentDialogueIndex++;

                    if (currentDialogueIndex >= dialogues.Count)
                    {
                        isDialogueActive = false;
                        currentDialogueIndex = 0;
                    }

                    currentCharacter = 0;
                    isTextComplete = false;
                    currentWaitTime = 0f;
                }
            }
        }

        public string GetCurrentText()
        {
            return dialogues[currentDialogueIndex].Substring(0, currentCharacter);
        }

        public bool IsDialogueActive() => isDialogueActive;
    }

}
