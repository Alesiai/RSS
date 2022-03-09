import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ChannelsService {
  channels: string[];

  constructor(private http: HttpClient) { }

  getChannels() {
    return this.http.get<string[]>('https://localhost:5001/channels').pipe(
      map(chhannels => {
        this.channels= chhannels;
        return chhannels;
      })
    )
  }
}
