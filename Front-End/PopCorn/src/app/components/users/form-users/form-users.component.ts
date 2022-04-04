import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AlertNotificationService } from 'src/app/services/alertNotification.service';
import { UserService } from 'src/app/services/user.service';
import { Global } from 'src/shared/Global';

@Component({
  selector: 'app-form-users',
  templateUrl: './form-users.component.html',
  styleUrls: ['./form-users.component.css']
})
export class FormUsersComponent implements OnInit {

  data: any;
  user: User;
  isUpdate: boolean = false;
  registerForm: FormGroup;

  constructor(
    private router: Router,
    private userService: UserService,
    private notificationService: AlertNotificationService
  ) {
    const nav = this.router.getCurrentNavigation();
    this.data = nav.extras.state;
   }

  ngOnInit(): void {
    if (this.data.operation == "create") {
      this.createForm();
    }else if(this.data.operation == "update"){
      this.isUpdate = true;
      this.createForm();
      this.registerForm.controls['confirmaPassword'].setValue(this.data.password);
      this.registerForm.setValue(this.data.user);
    }else if(this.data.operation == "delete"){
      this.createFormDisabled();
      this.registerForm.setValue(this.data.user);
    }
  }

  onSubmit(){
    if (this.data.operation == "create") {
      this.createUser();
    }else if(this.data.operation == "update"){
      this.updateUser();
    }else if(this.data.operation == "delete"){
      this.deleteUser();
    }
  }

  createForm(){
    this.registerForm = new FormGroup({
      id: new FormControl(),
      userName: new FormControl('', [Validators.required]),
      role: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirmarPassword: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      dateCreate: new FormControl({value: '', disabled: true})
    });
  }

  createFormDisabled(){
    this.registerForm = new FormGroup({
      id: new FormControl(),
      userName: new FormControl({value: '', disabled: true}),
      role: new FormControl({value: '', disabled: true}),
      password: new FormControl({value: '', disabled: true}),
      confirmarPassword: new FormControl({value: '', disabled: true}),
      email: new FormControl({value: '', disabled: true}),
      dateCreate: new FormControl({value: '', disabled: true}),
    });
  }

  createUser(){
    if (this.registerForm.controls['password'].value == this.registerForm.controls['confirmarPassword'].value) {
      if(this.registerForm.valid){
        this.user = Object.assign({}, this.registerForm.value);
        //this.validateStart(this.session.startTime);
        this.user.id = 0;
        this.user.dateCreate = new Date();
        this.userService.Create(`${Global.BASE_URL_API}/user`, this.user).subscribe({
          next: userReturn =>{
            this.notificationService.showMessage(userReturn.message, userReturn.type);
            this.router.navigateByUrl("load", {state: {route: 'users'}});
          },
          error: userError => {
            this.notificationService.showMessage(userError.message, userError.type);
            this.router.navigateByUrl("load", {state: {route: 'users'}});
          }
        })
      }else {
        this.notificationService.showMessage("Por favor, preencha todos os campos!", "warning");
      }
    }else{
      this.notificationService.showMessage("Os campos SENHA e CONFIMAR SENHA são diferentes!", "warning");
    }

  }

  updateUser(): void {
    if (this.registerForm.controls['password'].value == this.registerForm.controls['confimarPassword']) {
      this.user = Object.assign({}, this.registerForm.value);
      this.user.id = this.data.user.id;
  
      this.userService.Update(`${Global.BASE_URL_API}/movie`, this.user)
        .subscribe({
          next: userReturn => {
            this.notificationService.showMessage(userReturn.message, userReturn.type);
            this.router.navigateByUrl("load", {state: {route: 'users'}});
          },
          error: userError => {
            this.notificationService.showMessage(userError.message, userError.type);
            this.router.navigateByUrl("load", {state: {route: 'users'}});
          }
        });
    }else{
      this.notificationService.showMessage("Os campos SENHA e CONFIMAR SENHA são diferentes!", "warning");
    }
  }

  deleteUser(){
    this.user = Object.assign({}, this.registerForm.value);
    this.user.id = this.data.user.id;

    this.userService.Delete(`${Global.BASE_URL_API}/user`, Number(this.user.id)).subscribe({
      next: userReturn => {
        this.notificationService.showMessage(userReturn.message, userReturn.type);
        this.router.navigateByUrl("load", {state: {route: 'users'}});
      },
      error: userError => {
        this.notificationService.showMessage(userError.message, userError.type);
        this.router.navigateByUrl("load", {state: {route: 'users'}});
      }
    })
  }

  formatDate(){
    var data = new Date(),
        dia  = data.getDate().toString(),
        diaF = (dia.length == 1) ? '0'+dia : dia,
        mes  = (data.getMonth()+1).toString(), //+1 pois no getMonth Janeiro começa com zero.
        mesF = (mes.length == 1) ? '0'+mes : mes,
        anoF = data.getFullYear();
    return diaF+"/"+mesF+"/"+anoF;
  }

}
