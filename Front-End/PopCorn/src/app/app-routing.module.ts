import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoadComponent } from "./components/load/load.component";
import { LoginComponent } from "./components/login/login.component";
import { FormMoviesComponent } from "./components/movies/form-movies/form-movies.component";
import { ListMoviesComponent } from "./components/movies/list-movies/list-movies.component";
import { RoomsComponent } from "./components/rooms/rooms.component";
import { FormSessionsComponent } from "./components/sessions/form-sessions/form-sessions.component";
import { ListSessionsComponent } from "./components/sessions/list-sessions/list-sessions.component";
import { FormUsersComponent } from "./components/users/form-users/form-users.component";
import { ListUsersComponent } from "./components/users/list-users/list-users.component";
import { AuthGuard } from "./guards/auth.guard";

const routes: Routes = [
    {canActivate: [AuthGuard], path: '', component: ListMoviesComponent},
    {canActivate: [AuthGuard], path: 'movie', component: FormMoviesComponent},
    {canActivate: [AuthGuard], path: 'rooms', component: RoomsComponent},
    {canActivate: [AuthGuard], path: 'sessions', component: ListSessionsComponent},
    {canActivate: [AuthGuard], path: 'session', component: FormSessionsComponent},
    {canActivate: [AuthGuard], path: 'users', component: ListUsersComponent},
    {canActivate: [AuthGuard], path:  'user', component: FormUsersComponent},
    {path: 'load', component: LoadComponent},
    {path: 'login', component: LoginComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule{

}