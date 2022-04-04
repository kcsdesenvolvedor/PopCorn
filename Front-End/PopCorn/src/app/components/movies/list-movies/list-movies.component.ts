import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from 'src/app/models/movie';
import { MovieService } from 'src/app/services/movie.service';
import { Global } from 'src/shared/Global';

@Component({
  selector: 'app-list-movies',
  templateUrl: './list-movies.component.html',
  styleUrls: ['./list-movies.component.css']
})
export class ListMoviesComponent implements OnInit {

  movies: Movie[] = [];
  displayedColumns = ['id', 'title', 'description', 'duration', 'action']

  constructor(
    private movieService: MovieService,
    private _router: Router) { }

  ngOnInit(): void {
    this.movieService.GetMovies(`${Global.BASE_URL_API}/movie`).subscribe(movie => {
      this.movies = movie;
      console.log(this.movies);
    });
  }

  update(id: number) {
    const movie = this.movies.filter(m => m.id === id)[0];
    this._router.navigateByUrl("movie", { state: { title: "Atualizar Filme", btnTitle: "Atualizar", operation: 'update', movie: movie } })
  }

  delete(id: number){
    const movie = this.movies.filter(m => m.id === id)[0];
    this._router.navigateByUrl("movie", { state: { title: "Deletar Filme", btnTitle: "Deletar", operation: 'delete', movie: movie } })
  }

  create(): void {
    this._router.navigateByUrl("movie", {state: { title: 'Novo Filme', btnTitle: 'Adicionar', operation: 'create' }});
  }

}
