using UnityEngine;
using System.Collections;

public class TaskInteractionHandler : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool isInteractable;
    Task task;


    // Use this for initialization
    void Start()
    {
        this.task = GameStateManager.lastAddedTask;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.isInteractable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.task.isComplete)
        {
            foreach (PlayerController player in GameStateManager.players)
            {
                if (this.IsWithinInteractionRange(player)) {
                    this.isInteractable = true;
                    spriteRenderer.color = new Color(1, 0, 0, 0.5f);
                } else
                {
                    this.isInteractable = false;
                    spriteRenderer.color = new Color(1, 0, 0, 1);
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (!this.task.isComplete && isInteractable && this.task.isInteracting)
        {
            if (this.task.interactingClock >= Task.COMPLETION_TIME)
            {
                this.task.isComplete = true;
                GameStateManager.tasks.Remove(this.task);
                GameStateManager.score++;
                Object.Destroy(this.gameObject);
            }
            this.task.interactingClock += Time.deltaTime;
        }
        Debug.Log(this.task.interactingClock);
    }

    private void OnMouseDown()
    {
        if (!this.task.isComplete && isInteractable)
        {
            this.task.isInteracting = true;
        }
    }

    private void OnMouseExit()
    {
        if (!this.task.isComplete)
        {
            this.task.isInteracting = false;
            this.task.interactingClock = 0;
        }
    }

    private void OnMouseUp()
    {
        if (!this.task.isComplete)
        {
            this.task.isInteracting = false;
            this.task.interactingClock = 0;
        }
    }

    public bool IsWithinInteractionRange(PlayerController player)
    {
        return Vector2.Distance(new Vector2(player.globalX, player.globalY), new Vector2(this.task.globalX, this.task.globalY)) < Task.INTERACTION_RANGE;
    }
}
