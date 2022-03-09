import { Component, OnInit} from '@angular/core';
import { Item } from './models/item';
import { ChannelsService } from './services/channels.service';
import { ItemsService } from './services/items.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{ 
  items: Item[];
  countOfPages: string;
  page: number = 1;
  channel: string = 'all';
  sorted: string  = '';
  pageInfo: any;
  totalPages: number;
  channels: string[];
  selected: string = "all";

  constructor(private itemService: ItemsService, private channelService:  ChannelsService) {}
  
  ngOnInit(): void {
    this.getPages();
    this.getItems();
    this.getChannels();
  }

  getItems(){
    this.page =1;
    this.sorted = '';
    this.itemService.getMembers(this.page, this.channel, this.sorted).subscribe(response => {
      this.items = response;
    })
  }

  getPages(){
    this.itemService.getPageInfo(this.page, this.channel, this.sorted).subscribe(response => {
      this.pageInfo = response;
    });
  }

  getChannels(){
    this.channelService.getChannels().subscribe(respnse => {
      this.channels = respnse;
    })
  }

  getItemsSortedByDate(){
    this.sorted = 'date';
    this.page =1;
    this.itemService.getMembers(this.page, this.channel, this.sorted).subscribe(response => {
      this.items = response;
    })
  }

  getItemsSortedByChannel(){
    this.sorted = 'channel';
    this.page =1;
    this.itemService.getMembers(this.page, this.channel, this.sorted).subscribe(response => {
      this.items = response;
    })
  }
  getPrevPage(){
    if(this.page > 1) {
      this.page--;
    }
    this.itemService.getMembers(this.page, this.channel, this.sorted).subscribe(response => {
      this.items = response;
    })

  }

  getNextPage(){
    this.totalPages = this.itemService.totalPages;
    if(this.page < this.totalPages) {
      this.page++;
    }
    this.itemService.getMembers(this.page, this.channel, this.sorted).subscribe(response => {
      this.items = response;
    })
  }

  getByChannelName(){
    this.channel = this.selected;
    this.page =1;
    this.getPages();

    this.itemService.getMembers(this.page, this.channel, this.sorted).subscribe(response => {
      this.items = response;
    })
  }
}