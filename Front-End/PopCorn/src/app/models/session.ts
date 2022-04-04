import { AnimationType } from "../enums/animationType";
import { AudioType } from "../enums/audioType";

export class Session {
    id: number;
    sessionDate: Date;
    startTime: string;
    endTime: string;
    ticketValue: number;
    typeAnimation: AnimationType;
    typeAudio: AudioType;
    movieId: number;
    roomId: number;
  }