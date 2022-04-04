import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Global } from 'src/shared/Global';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.css']
})
export class ListUsersComponent implements OnInit {

  users: User[] = [];
  displayedColumns = ['id', 'userName', 'role', 'password', 'email', 'dateCreate', 'action'];

  constructor(
    private userService: UserService,
    private _router: Router) { }

  ngOnInit(): void {
    this.userService.GetUsers(`${Global.BASE_URL_API}/user`).subscribe(users => {
      this.users = users;
    });
  }

  update(id: number) {
    const user = this.users.filter(u => u.id === id)[0];
    this._router.navigateByUrl("user", { state: { title: "Atualizar Usuário", btnTitle: "Atualizar", operation: 'update', user: user } })
  }

  delete(id: number){
    const user = this.users.filter(m => m.id === id)[0];
    this._router.navigateByUrl("user", { state: { title: "Deletar Usuário", btnTitle: "Deletar", operation: 'delete', user: user } })
  }

  create(): void {
    this._router.navigateByUrl("user", {state: { title: 'Novo Usuário', btnTitle: 'Adicionar', operation: 'create' }});
  }
}
