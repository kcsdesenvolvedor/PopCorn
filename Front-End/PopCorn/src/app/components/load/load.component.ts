import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-load',
  templateUrl: './load.component.html',
  styleUrls: ['./load.component.css']
})
export class LoadComponent implements OnInit {

  data: any;

  constructor(private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.data = nav.extras.state;
   }

  ngOnInit(): void {
    this.reLoad(this.data.route);
  }

  reLoad(route: string){
    setTimeout(()=>{
      this.router.navigateByUrl(route);
    }, 2000);
  }

}
