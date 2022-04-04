import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from 'src/app/models/movie';
import { Session } from 'src/app/models/session';
import { AlertNotificationService } from 'src/app/services/alertNotification.service';
import { MovieService } from 'src/app/services/movie.service';
import { SessionService } from 'src/app/services/session.service';
import { Global } from 'src/shared/Global';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-list-sessions',
  templateUrl: './list-sessions.component.html',
  styleUrls: ['./list-sessions.component.css']
})
export class ListSessionsComponent implements OnInit {

  sessions: Session[] = [];
  movies: Movie[] = [];
  displayedColumns = ['id', 'sessionDate', 'startTime', 'endTime', 'ticketValue', 'typeAnimation', 'typeAudio', 'roomId', 'action'];

  constructor(
    private sessionService: SessionService,
    private movieService: MovieService,
    private router: Router,
    private alertNotificationService: AlertNotificationService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.sessionService.getSessions(`${Global.BASE_URL_API}/session`).subscribe({
      next: sessionReturn => {
        this.sessions = sessionReturn;
      },
      error: () => {
        this.alertNotificationService.showMessage("Não existe sessões cadastrada", "error");
      }
    })

    this.movieService.GetMovies(`${Global.BASE_URL_API}/movie`).subscribe(moviesReturn => {
      this.movies = moviesReturn;
    })
  }

  create(){
    this.router.navigateByUrl("session", {state: {title: 'Adicionar Sessão', operation: 'create'}});
  }

  openInfo(sessionId: number, movieId: number, typeInfo: string){
    const session = this.sessions.filter(s => s.id == sessionId)[0];
    const movie = this.movies.filter(m => m.id == movieId)[0];


    this.dialog.open(DialogInfo, {
      data: {
        sessionId: session.id,
        sessionDate: session.sessionDate,
        startTime: session.startTime,
        endTime: session.endTime,
        ticketValue: session.ticketValue,
        typeAnimation: session.typeAnimation,
        typeAudio: session.typeAudio,
        movieTitle: movie.title,
        roomId: session.roomId,
        type: typeInfo
      }
    });
  }

}

@Component({
  selector: 'dialog-info',
  templateUrl: './dialog-info.html',
  styleUrls: ['./dialog-info.css']
})
export class DialogInfo {
  page: string = '';
  forceDelete: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<DialogInfo>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private sessionService: SessionService,
    private router: Router,
    private alertNotificationService: AlertNotificationService) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  deleteSession(id: number){
    this.sessionService.delete(`${Global.BASE_URL_API}/session`, id, this.forceDelete).subscribe({
      next: sessionReturn => {
        this.alertNotificationService.showMessage(sessionReturn.message, sessionReturn.type);
        this.onNoClick();
        this.router.navigateByUrl("load", {state: {route: 'sessions'}});
      },
      error: sessionError => {
        this.alertNotificationService.showMessage(sessionError.message, sessionError.type);
        this.onNoClick();
        this.router.navigateByUrl("load", {state: {route: 'sessions'}});
      }
    })
  }

  checked(){
    this.forceDelete = !this.forceDelete;
  }
}
