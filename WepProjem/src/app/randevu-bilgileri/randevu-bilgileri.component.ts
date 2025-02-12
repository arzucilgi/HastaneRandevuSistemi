  import { HttpClient } from '@angular/common/http';
  import { Component, OnInit } from '@angular/core';
  import { Router } from '@angular/router';

  @Component({
    selector: 'app-randevu-bilgileri',
    imports: [],
    templateUrl: './randevu-bilgileri.component.html',
    styleUrl: './randevu-bilgileri.component.css'
  })
  export class RandevuBilgileriComponent implements OnInit{
    constructor(private http: HttpClient) { }
    kullaniciBilgileri: any

    girisYapanKullaniciId = JSON.parse(localStorage.getItem('userId') || 'null');
    secilenSehir = JSON.parse(localStorage.getItem('secilenSehir') || '""');
    secilenHastane = JSON.parse(localStorage.getItem('secilenHastane') || '""');
    secilenPoliklinik = JSON.parse(localStorage.getItem('secilenPoliklinik') || '""');
    secilenDoktor = JSON.parse(localStorage.getItem('secilenDoktor') || '""')



    ngOnInit(): void {
    this.http.get<any[]>(`http://localhost:5153/api/Authentication/${this.girisYapanKullaniciId}`, { withCredentials: true }).subscribe({
      next: (data) => {
        console.log(data);  
        this.kullaniciBilgileri = data;   
      }
    
    })
    }
  
    

  }