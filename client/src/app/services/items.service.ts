import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Item } from '../models/item';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../models/pagination';


@Injectable({
  providedIn: 'root'
})
export class ItemsService implements OnInit {
  items: Item[] = [];
  pages: PaginatedResult[] = [];
  totalPages: number;
  result: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  getMembers(pageNumber: number, searchedChannel: string, orderBy: string) {
    return this.http.get<Item[]>('https://localhost:5001/items?orderBy='+orderBy+'&pageNumber='+pageNumber+'&pageSize=10&searchedChannel='+searchedChannel).pipe(
      map(members => {
        this.items= members;
        return members;
      })
    )
  }

  getPageInfo(pageNumber: number, searchedChannel: string, orderBy: string){
     
    this.result = this.http.get('https://localhost:5001/items/page?orderBy='+orderBy+'&pageNumber='+pageNumber+'&pageSize=10&searchedChannel='+searchedChannel).pipe(
      map((response: PaginatedResult) => {
        const user = response;
        this.totalPages = user.totalPages;
      })
    )
    
    return this.result;
  }
}


