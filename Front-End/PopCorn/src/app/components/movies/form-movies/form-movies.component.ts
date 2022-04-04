import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Movie } from 'src/app/models/movie';
import { AlertNotificationService } from 'src/app/services/alertNotification.service';
import { MovieService } from 'src/app/services/movie.service';
import { Global } from 'src/shared/Global';

@Component({
  selector: 'app-form-movies',
  templateUrl: './form-movies.component.html',
  styleUrls: ['./form-movies.component.css']
})
export class FormMoviesComponent implements OnInit {

  movie: Movie;
  data: any;
  registerForm: FormGroup;
  base64: string;
  jsonImage: object;

  constructor(
    private movieService: MovieService,
    private alertNotificationService: AlertNotificationService,
    private router: Router,
    private fb: FormBuilder) {
    const nav = this.router.getCurrentNavigation();
    this.data = nav.extras.state;
  }

  ngOnInit(): void {
    if (this.data.operation == "create") {
      this.createForm();
      this.registerForm.reset();
    } else if (this.data.operation == "update") {
      this.createForm();
      this.loadImage();
    }else if(this.data.operation == 'delete'){
      this.createFormDisable();
      this.loadImage();
    }
  }

  loadImage() {
    this.registerForm.setValue(this.data.movie);

    this.registerForm.controls['image'].reset();

    let imageUpdate = this.data.movie.image;
    let jsonUpdate = JSON.parse(imageUpdate);

    let span = document.getElementById("nameImage");
    let text = document.createTextNode("Imagem atual: " + jsonUpdate["name"]);
    span.appendChild(text);

    this.base64 = jsonUpdate["base64"];

    this.jsonImage = this.movieService.mountImage(jsonUpdate["name"], jsonUpdate["base64"]);
  }

  onSubmit() {
    if (this.data.operation == 'create') {
      this.createMovie();
    } else if(this.data.operation == 'update') {
      this.updateMovie();
    } else if(this.data.operation == 'delete'){
      this.deleteMovie();
    }
  }

  createMovie(): void {
    if(this.validationDuration()){
      if (this.registerForm.valid && this.registerForm.controls['image'].value != null) {
        this.movie = Object.assign({}, this.registerForm.value);
        this.movie.id = 0;
        this.movie.image = JSON.stringify(this.jsonImage);
        this.movieService.Create(`${Global.BASE_URL_API}/movie`, this.movie).subscribe({
          next: movieReturn => {
            this.alertNotificationService.showMessage(movieReturn.message, movieReturn.type);
            this.router.navigateByUrl('');
          },
          error: errorReturn => {
            this.alertNotificationService.showMessage(errorReturn.message, errorReturn.type);
            this.router.navigateByUrl('');
          }
  
        })
      } else {
        this.alertNotificationService.showMessage("Por favor, preencha todos os campos!", "warning");
      }
    }else{
      this.alertNotificationService.showMessage("Duração do filme está no formato errado!", 'warning');
    }


  }

  validationDuration(){
    const durationMovie = this.registerForm.controls['duration'].value;
    const durationSplit = durationMovie.split(':');
    if(durationSplit.length == 2 && !isNaN(durationSplit[0]) && !isNaN(durationSplit[1])){
      return true;
    }
    
    return false;

  }

  updateMovie(): void {
    this.movie = Object.assign({}, this.registerForm.value);
    this.movie.id = this.data.movie.id;
    this.movie.image = JSON.stringify(this.jsonImage);

    this.movieService.Update(`${Global.BASE_URL_API}/movie`, this.movie)
      .subscribe({
        next: movieReturn => {
          this.alertNotificationService.showMessage(movieReturn.message, movieReturn.type);
          this.router.navigateByUrl('');
        },
        error: movieError => {
          this.alertNotificationService.showMessage(movieError.message, movieError.type);
          this.router.navigateByUrl('');
        }
      });
  }

  deleteMovie(): void {
    this.movie = Object.assign({}, this.registerForm.value);
    this.movie.id = this.data.movie.id;

    this.movieService.Delete(`${Global.BASE_URL_API}/movie`, this.movie.id)
      .subscribe({
        next: movieReturn => {
          this.alertNotificationService.showMessage(movieReturn.message, movieReturn.type);
          this.router.navigateByUrl('');
        },
        error: movieError => {
          this.alertNotificationService.showMessage(movieError.message, movieError.type);
          this.router.navigateByUrl('');
        }
      })
  }

  cancel(): void {
    this.registerForm.reset();
    this.router.navigateByUrl('');
  }

  createForm() {
    this.registerForm = this.fb.group({
      id: [""],
      title: ["", Validators.required],
      description: ["", Validators.required],
      duration: ["", Validators.required],
      image: ["", Validators.required]

    });

  }

  createFormDisable() {
    this.registerForm = new FormGroup({
      id: new FormControl({ value: '', disabled: true }),
      title: new FormControl({ value: '', disabled: true }),
      description: new FormControl({ value: '', disabled: true }),
      duration: new FormControl({ value: '', disabled: true }),
      image: new FormControl({ value: '', disabled: true })
    });
  }

  changeFile(e) {
    this.jsonImage = this.movieService.mountImage(e[0].name, e[0].base64);
    this.base64 = this.jsonImage['base64'];
  }

}
