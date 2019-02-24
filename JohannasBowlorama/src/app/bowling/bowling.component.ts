import { Component, OnInit } from '@angular/core';
import { BowlingService } from '../bowling.service';

import { Game } from '../game';
import { Frame } from '../frame';

@Component({
  selector: 'app-bowling',
  templateUrl: './bowling.component.html',
  styleUrls: ['./bowling.component.css'],
  providers: [BowlingService]
})
export class BowlingComponent implements OnInit {
  game: Game = null;
  firstRoll: number = 0;
  secondRoll: number = 0;
  thirdRoll: number = 0;
  showthirdInput: boolean = false;
  showModal: boolean = false;
  showNewGameModal: boolean = false;
  errorText:string = "";
  playerName: string = "";

  constructor(private bowlingService: BowlingService) { }

  newGame = function()
  {
    this.showModal = true;
    this.showthirdInput = false;
    this.firstRoll = 0;
    this.secondRoll = 0;
    this.thirdRoll = 0;


    this.bowlingService.startNewGame(this.playerName)
      .subscribe(
        (data: Game) =>
        {
          this.game = data;
          this.game.Frames = this.prettifyData(this.game.Frames);
          this.showModal = false;
        },
        err => {
          this.showModal = false;
          this.showErrorModal = true;
          if(err.error.Message)
            this.errorText = err.error.Message;
          else
            this.errorText = err.error;
        }
      );
  }

  submitRolls = function()
  {
    this.showModal = true;
    this.bowlingService.addFrame(this.game.Id,{roll1:this.firstRoll,roll2:this.secondRoll, roll3:this.thirdRoll})
      .subscribe(
        (data: Game) =>
        {
          this.game = data;
          if(this.game.Frames.length == 9)
            this.showthirdInput = true;;
          this.game.Frames = this.prettifyData(this.game.Frames);
          this.showModal = false;
        },
        err => {
          this.showModal = false;
          this.showErrorModal = true;
          if(err.error.Message)
            this.errorText = err.error.Message;
          else
            this.errorText = err.error;
        }
      );
  }

  acceptError = function()
  {
    this.showErrorModal = false;
    this.errorText = "";
  }

  prettifyData = function(frames)
  {
    for(let i = 0; i < frames.length; i++)
    {
      let frame = frames[i];
      if(frame.Roll1 == 10 && i != 9)
      {
        frame.Roll2 = -1;
      }
      else if(frame.Roll1 + frame.Roll2 == 10 && i != 9)
        frame.Roll2 = 11;
    }
    if(frames.length < 10)
      frames = this.addEmptyFrames(frames);
    return frames;
  }

  addEmptyFrames = function(frames)
  {
    for(let i = frames.length; i < 10; i++)
    {
      let frame = new Frame();
      frame.Roll1 = -1;
      frame.Roll2 = -1;
      if(i == 9)
        frame.Roll3 = -1;
      frames.push(frame);
    }
    return frames;
  }

  ngOnInit() {
  }
}
