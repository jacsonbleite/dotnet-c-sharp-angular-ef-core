import { HttpClient } from '@angular/common/http';
import { Component, } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent {
  public eventos: any = [];
  public filteredEvents: any = [];

  widthImg: number = 70;
  marginImg: number = 2;
  showImg: boolean = true;
  private _filterList: string = '';

  public get filterList(): string {
    return this._filterList;
  }

  public set filterList(value: string) {
    this._filterList = value;
    this.filteredEvents = this.filterList ? this.filterEvents(this.filterList) : this.eventos;
  }

  filterEvents(filterBy: string): any{
    filterBy = filterBy.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string; }) => evento.tema.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  constructor(private http: HttpClient) {}

  ngOnInit(): void{
    this.getEventos();
  }

  showOrHideImage() {
    this.showImg = !this.showImg;
  }

  public getEventos(): void {

    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response;
        this.filteredEvents = this.eventos;
      },
      error => console.log(error)
    );

  }



}
