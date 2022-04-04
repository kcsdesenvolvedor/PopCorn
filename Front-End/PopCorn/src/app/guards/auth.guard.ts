import { AuthService } from '../services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Global } from 'src/shared/Global';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router, private http: HttpClient) { }

  canActivate(): boolean {
    if (this.authService.logedIn()) { // verifica se o token ja foi expirado e retona para pagina de login
      const responseAuthorization = this.http.get(`${Global.BASE_URL_API}/room/Alls`);
      responseAuthorization.subscribe({
        error: response => {
          if(response.status === 401){
            this.authService.logout();
            console.log('deslogou!');
          }
        }
      })
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
