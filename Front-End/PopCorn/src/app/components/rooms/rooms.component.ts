import { Component, OnInit } from '@angular/core';
import { Room } from 'src/app/models/room';
import { RoomService } from 'src/app/services/room.service';
import { Global } from 'src/shared/Global';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {

  rooms: Room[];
  displayedColumns = ['column-id', 'column-name', 'column-numberOfSeats'];

  constructor(
    private roomService: RoomService
  ) { }

  ngOnInit(): void {
    this.roomService.getRoomsAlls(`${Global.BASE_URL_API}/room/alls`).subscribe(roomsReturn => {
      this.rooms = roomsReturn;
    })
  }

}
