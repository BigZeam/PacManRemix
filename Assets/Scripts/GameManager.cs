using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variable declaration
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;


    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set;}
    public int lives { get; private set;}

    void Start()
    {
        NewGame();

    }


    void Update()
    {
        if(this.lives <= 0 && Input.anyKeyDown)
        {
           NewGame();
        }
    }

    void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }
    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(true);
        } 
        this.pacman.gameObject.SetActive(true);
    }
    private void GameOver()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(false);
        }
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        } 
        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = this.score + (ghost.points) * ghostMultiplier;
        SetScore(points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);
        SetLives(this.lives -1);
        if(this.lives>0){
            Invoke(nameof(ResetState), 3.0f);
        } else {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.score + pellet.points);

        if(!CheckRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }

    }
    public void PowerPelletEaten(PowerPellet pellet)
    {
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool CheckRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if(pellet.gameObject.activeSelf)
            {
                return true;
            }
            
        }
        return false;
    }
    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
