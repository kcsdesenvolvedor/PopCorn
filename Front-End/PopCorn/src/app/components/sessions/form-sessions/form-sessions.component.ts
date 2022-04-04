import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Movie } from 'src/app/models/movie';
import { Room } from 'src/app/models/room';
import { Session } from 'src/app/models/session';
import { AlertNotificationService } from 'src/app/services/alertNotification.service';
import { MovieService } from 'src/app/services/movie.service';
import { RoomService } from 'src/app/services/room.service';
import { SessionService } from 'src/app/services/session.service';
import { Global } from 'src/shared/Global';

@Component({
  selector: 'app-form-sessions',
  templateUrl: './form-sessions.component.html',
  styleUrls: ['./form-sessions.component.css']
})
export class FormSessionsComponent implements OnInit {

  registerForm: FormGroup;
  data: any;
  session: Session;
  movies: Movie[];
  rooms: Room[];
  dateLimited: any;

  constructor(
    private sessionService: SessionService,
    private movieService: MovieService,
    private roomService: RoomService,
    private router: Router,
    private alertNotificationService: AlertNotificationService,
    private fb: FormBuilder
  ) {
    const nav = this.router.getCurrentNavigation();
    this.data = nav.extras.state;
   }

  ngOnInit(): void {
    this.createForm();
    this.getMovies();
    this.registerForm.reset();
    this.limitedDate();
    
  }

  createForm(){

    this.registerForm = this.fb.group({
      id: [""],
      sessionDate: ["", Validators.required],
      startTime: ["", Validators.required],
      endTime: [""],
      ticketValue: ["", Validators.required],
      typeAnimation: ["", Validators.required],
      typeAudio: ["", Validators.required],
      movieId: ["", Validators.required],
      roomId: [""]
    })
  }

  createSession(){
    if(this.registerForm.valid && this.registerForm.controls['roomId'].value != null){
      this.session = Object.assign({}, this.registerForm.value);
      //this.validateStart(this.session.startTime);
      this.session.id = 0;
      this.session.endTime = "0",
      this.session.typeAnimation = parseInt(this.registerForm.controls['typeAnimation'].value);
      this.session.typeAudio = parseInt(this.registerForm.controls['typeAudio'].value);

      this.sessionService.create(`${Global.BASE_URL_API}/session`, this.session).subscribe({
        next: sessionReturn =>{
          this.alertNotificationService.showMessage(sessionReturn.message, sessionReturn.type);
          this.router.navigateByUrl('sessions');
        },
        error: sessionError => {
          this.alertNotificationService.showMessage(sessionError.message, sessionError.type);
          this.router.navigateByUrl('sessions');
        }
      })
    }else {
      this.alertNotificationService.showMessage("Por favor, preencha todos os campos!", "warning");
    }

  }

  getMovies(){
    this.movieService.GetMovies(`${Global.BASE_URL_API}/movie`).subscribe(moviesReturn => {
      this.movies = moviesReturn;
    })
  }

  getRoomsBySession(){
    if(this.registerForm.valid){
      this.roomService.getRoomsBySession(this.registerForm).subscribe(roomsReturn => {
        this.rooms = roomsReturn;
      })
    }else {
      this.alertNotificationService.showMessage("Por favor, preencha todos os campos!", "warning");
    }
  }

  cancel(){
    this.router.navigateByUrl('sessions');
  }

  limitedDate() {
    this.dateLimited = this.sessionService.limitedDate();
  }

  validateStart(){
    let start = this.registerForm.controls['startTime'].value;
    let splitStart = start.split(':');
    let splitStart1 = splitStart[0];
    let splitStart2 = splitStart[1];

    let newStart1 = splitStart1.length == 1 ? `0${splitStart1}` : `${splitStart1}`;
    let newStart2 = splitStart2.length == 1 ? `${splitStart2}0` : `${splitStart2}`;
    let newStart = `${newStart1}:${newStart2}`;

    console.log(newStart);
    //return newStart;
  }

}
