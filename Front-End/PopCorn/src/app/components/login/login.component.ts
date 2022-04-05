import { AlertNotificationService } from 'src/app/services/alertNotification.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Global } from 'src/shared/Global';
import { UserAccess } from 'src/app/models/userAcess';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  returnUrl: string;
  formLogin: FormGroup;
  model: any = {};
  userAccess: UserAccess;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertNotificationService: AlertNotificationService) { }

  ngOnInit() {
    this.formLogin = this.fb.group({     // {5}
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  get f() { return this.formLogin.controls; }

  login() {
    this.authService.login(this.GetUserAccess(), `${Global.BASE_URL_API + "/account/login"}`).subscribe({
      next: n => {
        /* this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/movies';
        this.router.navigate([this.returnUrl]);
        this.router.navigateByUrl('movies'); */
        document.location.assign(`${Global.BASE_URL_LOCAL}`);
        
      },
      error: e => {
        this.alertNotificationService.showMessage(e.error.message, e.type);
        console.log("Deu erro");
      }
    })
    /* console.log('chamou');
    this.authService.login(this.GetUserAccess(), `${Global.BASE_USER_ENDPOINT + "/account/login"}`).subscribe(() => {
      console.log("Logado");
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/movies';
      this.router.navigate([this.returnUrl]);
    }, erro => {
      this.alertNotificationService.showMessage(erro, 'info');
      console.log("Deu erro");
    }) */
  }

  cancel(): void {
    //this.router.navigateByUrl('/home');
    this.formLogin.reset();
  }

  GetUserAccess(): UserAccess {
    const user = new UserAccess();
    user.userName = this.f['userName'].value;
    user.password = this.f['password'].value;
    user.email = this.f['userName'].value;
    return user;
  }
}
