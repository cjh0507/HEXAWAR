using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{   
    // Reference to the BlockSystem script
    private BlockSystem blockSys;

    // Variables to hold data regarding current block type
    private int currentBlockId = 0;
    private Block currentBlock;

    // Variables for the block template.
    private GameObject blockTemplate;
    private SpriteRenderer currentRend;

    // Bools to control building system
    private bool buildModeOn = false;

    private void Awake() 
    {
        // Store referecne to block System script.
        blockSys = GetComponent<BlockSystem>();
    }

    void Update()
    {
        
        // If R key pressed, toggle build mode.
        if(Input.GetKeyDown("r"))
        {
            // Flip bool
            buildModeOn = !buildModeOn;

            // If we have a current template, destroy it
            if(blockTemplate != null)
            {
                Destroy(blockTemplate);
            }
            // If we don't have a current block type set.
            if(currentBlock == null)
            {
                // Ensure allBlocks array is ready.
                if (blockSys.allBlocks[currentBlockId] != null)
                {
                    // Get a new currentBlock using the ID variable.
                    currentBlock = blockSys.allBlocks[currentBlockId];
                }
            }

            if(buildModeOn)
            {
                // Create a new object for blockTemplate
                blockTemplate = new GameObject("CurrentBlockTemplate");
                // Add and store reference to a SpriteRenderer on the template object.
                currentRend = blockTemplate.AddComponent<SpriteRenderer>();
                // Set the sprite of the template object to match current block type.
                currentRend.sprite = currentBlock.blockSprite;
            }

        }

    }

}
